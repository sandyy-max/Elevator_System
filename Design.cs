using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Lift
{
    public partial class Design : Form
    {
        // Elevator object (Single Responsibility - handles logic)
        private Elevator elevator;

        // Animation state variables
        private bool doorAnimating = false;
        private bool liftAnimating = false;
        private string doorAction = "            "; 

        // Door positions for animation
        private const int DOOR_SPEED = 3;  
        private const int LIFT_SPEED = 1; 

        // Lift positions
        private int liftTargetY;
        private const int FLOOR1_Y = 405; 
        private const int FLOOR2_Y = 48;  

        // Door open positions for each floor
        private const int FLOOR1_LEFT_DOOR_OPEN = 140;
        private const int FLOOR1_RIGHT_DOOR_OPEN = 325;
        private const int FLOOR1_LEFT_DOOR_CLOSED = 195;
        private const int FLOOR1_RIGHT_DOOR_CLOSED = 271;

        private const int FLOOR2_LEFT_DOOR_OPEN = 230;
        private const int FLOOR2_RIGHT_DOOR_OPEN = 413;
        private const int FLOOR2_LEFT_DOOR_CLOSED = 284;
        private const int FLOOR2_RIGHT_DOOR_CLOSED = 359;

 
        private System.Windows.Forms.Timer doorCloseTimer;
        private const int DOOR_OPEN_DURATION = 3000;


        private BackgroundWorker dbWorker;

        public Design()
        {
            InitializeComponent();
            InitializeElevator();
            InitializeDoorCloseTimer();
            InitializeBackgroundWorker();
        }


        private void InitializeElevator()
        {
            try
            {
                elevator = new Elevator();

                
                elevator.FloorChanged += Elevator_FloorChanged;
                elevator.DoorsOpened += Elevator_DoorsOpened;
                elevator.DoorsClosed += Elevator_DoorsClosed;
                elevator.MovementStarted += Elevator_MovementStarted;
                elevator.MovementCompleted += Elevator_MovementCompleted;
                elevator.ErrorOccurred += Elevator_ErrorOccurred;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize elevator: {ex.Message}", "Initialization Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeDoorCloseTimer()
        {
            try
            {
                doorCloseTimer = new System.Windows.Forms.Timer();
                doorCloseTimer.Interval = DOOR_OPEN_DURATION;
                doorCloseTimer.Tick += DoorCloseTimer_Tick;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Timer initialization error: {ex.Message}");
            }
        }

        private void InitializeBackgroundWorker()
        {
            try
            {
                dbWorker = new BackgroundWorker();
                dbWorker.WorkerReportsProgress = true;
                dbWorker.WorkerSupportsCancellation = true;
                dbWorker.DoWork += DbWorker_DoWork;
                dbWorker.ProgressChanged += DbWorker_ProgressChanged;
                dbWorker.RunWorkerCompleted += DbWorker_RunWorkerCompleted;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BackgroundWorker initialization error: {ex.Message}");
            }
        }

        #region BackgroundWorker Events 

        private void DbWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                string operation = e.Argument as string;

                if (operation == "LoadLogs")
                {
                    // Simulate database operation
                    worker.ReportProgress(25, "Connecting to database...");
                    System.Threading.Thread.Sleep(200);

                    worker.ReportProgress(50, "Retrieving logs...");
                    DataSet ds = elevator.GetLogger().GetDatabaseHelper().GetAllLogs();

                    worker.ReportProgress(75, "Processing data...");
                    System.Threading.Thread.Sleep(100);

                    worker.ReportProgress(100, "Complete");
                    e.Result = ds;
                }
            }
            catch (Exception ex)
            {
                e.Result = new Exception($"Database operation failed: {ex.Message}");
            }
        }

        private void DbWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
            string message = e.UserState as string;
            System.Diagnostics.Debug.WriteLine($"Progress: {e.ProgressPercentage}% - {message}");
        }

        private void DbWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    MessageBox.Show($"Error: {e.Error.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Result is Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Result is DataSet ds)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        LogForm logForm = new LogForm(ds.Tables[0]);
                        logForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No logs found!", "Elevator Logs",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying logs: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void DoorCloseTimer_Tick(object? sender, EventArgs e)
        {
            try
            {
                doorCloseTimer.Stop();
                
                if (elevator.DoorsOpen && !liftAnimating && !doorAnimating)
                {
                    elevator.CloseDoors();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Timer tick error: {ex.Message}");
            }
        }

        #region Elevator Event Handlers

        private void Elevator_FloorChanged(object? sender, int floor)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => UpdateFloorDisplay(floor)));
                }
                else
                {
                    UpdateFloorDisplay(floor);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Floor changed event error: {ex.Message}");
            }
        }

        private void Elevator_DoorsOpened(object? sender, EventArgs e)
        {
            try
            {
                AnimateDoors("open");
                doorCloseTimer.Start();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Doors opened event error: {ex.Message}");
            }
        }

        private void Elevator_DoorsClosed(object? sender, EventArgs e)
        {
            try
            {
                doorCloseTimer.Stop();
                AnimateDoors("close");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Doors closed event error: {ex.Message}");
            }
        }

        private void Elevator_MovementStarted(object? sender, EventArgs e)
        {
            try
            {
                AnimateLift();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Movement started event error: {ex.Message}");
            }
        }

        private void Elevator_MovementCompleted(object? sender, EventArgs e)
        {
            // Doors will open automatically
        }

        private void Elevator_ErrorOccurred(object? sender, string errorMessage)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => MessageBox.Show(errorMessage, "Elevator Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)));
                }
                else
                {
                    MessageBox.Show(errorMessage, "Elevator Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error event handler error: {ex.Message}");
            }
        }

        #endregion

        #region Form Load

        private void main_Load(object sender, EventArgs e)
        {
            try
            {
                // Initialize display labels
                lblFloor1Status.Text = "Floor 1";
                lblFloor2Status.Text = "            ";
                lblDisplayWindow.Text = "Floor 1";

                // Position elevator at Floor 1
                liftBox.Top = FLOOR1_Y;

                // Close doors initially
                leftDoor1.Left = FLOOR1_LEFT_DOOR_CLOSED;
                rightDoor1.Left = FLOOR1_RIGHT_DOOR_CLOSED;
                leftDoor2.Left = FLOOR2_LEFT_DOOR_CLOSED;
                rightDoor2.Left = FLOOR2_RIGHT_DOOR_CLOSED;

                // Set timer intervals for smooth animation
                doorTimer.Interval = 30;
                liftTimer.Interval = 30;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form load error: {ex.Message}", "Initialization Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Animation Methods

        private void AnimateDoors(string action)
        {
            try
            {
                doorAction = action;
                doorAnimating = true;
                doorTimer.Start();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Animate doors error: {ex.Message}");
            }
        }

        private void AnimateLift()
        {
            try
            {
                liftTargetY = elevator.TargetFloor == 1 ? FLOOR1_Y : FLOOR2_Y;
                liftAnimating = true;
                liftTimer.Start();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Animate lift error: {ex.Message}");
            }
        }

        private void UpdateFloorDisplay(int floor)
        {
            try
            {
                lblDisplayWindow.Text = floor.ToString();

                if (floor == 1)
                {
                    lblFloor1Status.Text = "Floor 1";
                    lblFloor2Status.Text = "            ";
                }
                else
                {
                    lblFloor1Status.Text = "            ";
                    lblFloor2Status.Text = "Floor 2";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Update display error: {ex.Message}");
            }
        }

        #endregion

        #region Timer Events

        private void doorTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!doorAnimating) return;

                bool animationComplete = false;

                if (doorAction == "open")
                {
                    if (elevator.CurrentFloor == 1)
                    {
                        if (leftDoor1.Left > FLOOR1_LEFT_DOOR_OPEN)
                        {
                            leftDoor1.Left -= DOOR_SPEED;
                            rightDoor1.Left += DOOR_SPEED;
                        }
                        else
                        {
                            leftDoor1.Left = FLOOR1_LEFT_DOOR_OPEN;
                            rightDoor1.Left = FLOOR1_RIGHT_DOOR_OPEN;
                            animationComplete = true;
                        }
                    }
                    else
                    {
                        if (leftDoor2.Left > FLOOR2_LEFT_DOOR_OPEN)
                        {
                            leftDoor2.Left -= DOOR_SPEED;
                            rightDoor2.Left += DOOR_SPEED;
                        }
                        else
                        {
                            leftDoor2.Left = FLOOR2_LEFT_DOOR_OPEN;
                            rightDoor2.Left = FLOOR2_RIGHT_DOOR_OPEN;
                            animationComplete = true;
                        }
                    }
                }
                else if (doorAction == "close")
                {
                    if (elevator.CurrentFloor == 1)
                    {
                        if (leftDoor1.Left < FLOOR1_LEFT_DOOR_CLOSED)
                        {
                            leftDoor1.Left += DOOR_SPEED;
                            rightDoor1.Left -= DOOR_SPEED;
                        }
                        else
                        {
                            leftDoor1.Left = FLOOR1_LEFT_DOOR_CLOSED;
                            rightDoor1.Left = FLOOR1_RIGHT_DOOR_CLOSED;
                            animationComplete = true;
                        }
                    }
                    else
                    {
                        if (leftDoor2.Left < FLOOR2_LEFT_DOOR_CLOSED)
                        {
                            leftDoor2.Left += DOOR_SPEED;
                            rightDoor2.Left -= DOOR_SPEED;
                        }
                        else
                        {
                            leftDoor2.Left = FLOOR2_LEFT_DOOR_CLOSED;
                            rightDoor2.Left = FLOOR2_RIGHT_DOOR_CLOSED;
                            animationComplete = true;
                        }
                    }
                }

                if (animationComplete)
                {
                    doorTimer.Stop();
                    doorAnimating = false;
                }
            }
            catch (Exception ex)
            {
                doorTimer.Stop();
                doorAnimating = false;
                System.Diagnostics.Debug.WriteLine($"Door timer error: {ex.Message}");
            }
        }

        private void liftTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!liftAnimating) return;

                bool arrived = false;

                if (liftBox.Top < liftTargetY)
                {
                    liftBox.Top += LIFT_SPEED;
                    if (liftBox.Top >= liftTargetY)
                    {
                        liftBox.Top = liftTargetY;
                        arrived = true;
                    }
                }
                else if (liftBox.Top > liftTargetY)
                {
                    liftBox.Top -= LIFT_SPEED;
                    if (liftBox.Top <= liftTargetY)
                    {
                        liftBox.Top = liftTargetY;
                        arrived = true;
                    }
                }

                if (arrived)
                {
                    liftTimer.Stop();
                    liftAnimating = false;
                    elevator.UpdatePosition();
                }
            }
            catch (Exception ex)
            {
                liftTimer.Stop();
                liftAnimating = false;
                System.Diagnostics.Debug.WriteLine($"Lift timer error: {ex.Message}");
            }
        }

        #endregion

        #region Button Click Events - Request Buttons

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (elevator.CurrentFloor != 1)
                {
                    elevator.RequestFloor(1);
                }
                else
                {
                    elevator.OpenDoors();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Request Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (elevator.CurrentFloor != 2)
                {
                    elevator.RequestFloor(2);
                }
                else
                {
                    elevator.OpenDoors();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Request Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Button Click Events - Control Panel

        private void btnFloor1_Click(object sender, EventArgs e)
        {
            try
            {
                elevator.RequestFloor(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Request Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFloor2_Click(object sender, EventArgs e)
        {
            try
            {
                elevator.RequestFloor(2);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Request Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                doorCloseTimer.Stop();
                elevator.OpenDoors();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Door Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                doorCloseTimer.Stop();
                elevator.CloseDoors();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Door Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            try
            {
                // Use BackgroundWorker to load logs 
                if (!dbWorker.IsBusy)
                {
                    dbWorker.RunWorkerAsync("LoadLogs");
                }
                else
                {
                    MessageBox.Show("Loading logs, please wait...", "Busy",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading logs: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Unused Click Events

        private void rightDoor1_Click(object sender, EventArgs e) { }
        private void leftDoor1_Click(object sender, EventArgs e) { }
        private void rightDoor2_Click(object sender, EventArgs e) { }
        private void leftDoor2_Click(object sender, EventArgs e) { }
        private void floor2_Click(object sender, EventArgs e) { }
        private void liftBox_Click(object sender, EventArgs e) { }

        #endregion
    }
}




