using System;
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
        private string doorAction = ""; // "open" or "close"

        // Door positions for animation
        private const int DOOR_SPEED = 3;  // Slower door movement
        private const int LIFT_SPEED = 3;  // Slower lift movement

        // Lift positions
        private int liftTargetY;
        private const int FLOOR1_Y = 405; // Floor 1 lift position
        private const int FLOOR2_Y = 48;  // Floor 2 lift position

        // Door open positions for each floor
        private const int FLOOR1_LEFT_DOOR_OPEN = 140;
        private const int FLOOR1_RIGHT_DOOR_OPEN = 325;
        private const int FLOOR1_LEFT_DOOR_CLOSED = 195;
        private const int FLOOR1_RIGHT_DOOR_CLOSED = 271;

        private const int FLOOR2_LEFT_DOOR_OPEN = 230;
        private const int FLOOR2_RIGHT_DOOR_OPEN = 413;
        private const int FLOOR2_LEFT_DOOR_CLOSED = 284;
        private const int FLOOR2_RIGHT_DOOR_CLOSED = 359;

        // Timer for auto-closing doors
        private System.Windows.Forms.Timer doorCloseTimer;
        private const int DOOR_OPEN_DURATION = 3000; // 3 seconds

        public Design()
        {
            InitializeComponent();
            InitializeElevator();
            InitializeDoorCloseTimer();
        }

        /// <summary>
        /// Initialize elevator object and subscribe to events
        /// </summary>
        private void InitializeElevator()
        {
            elevator = new Elevator();

            // Subscribe to elevator events (Observer Pattern)
            elevator.FloorChanged += Elevator_FloorChanged;
            elevator.DoorsOpened += Elevator_DoorsOpened;
            elevator.DoorsClosed += Elevator_DoorsClosed;
            elevator.MovementStarted += Elevator_MovementStarted;
            elevator.MovementCompleted += Elevator_MovementCompleted;
        }

        /// <summary>
        /// Initialize timer for auto-closing doors
        /// </summary>
        private void InitializeDoorCloseTimer()
        {
            doorCloseTimer = new System.Windows.Forms.Timer();
            doorCloseTimer.Interval = DOOR_OPEN_DURATION;
            doorCloseTimer.Tick += DoorCloseTimer_Tick;
        }

        private void DoorCloseTimer_Tick(object? sender, EventArgs e)
        {
            doorCloseTimer.Stop();
            // Auto-close doors after they've been open for a while
            if (elevator.DoorsOpen && !liftAnimating && !doorAnimating)
            {
                elevator.CloseDoors();
            }
        }

        #region Elevator Event Handlers

        private void Elevator_FloorChanged(object? sender, int floor)
        {
            UpdateFloorDisplay(floor);
        }

        private void Elevator_DoorsOpened(object? sender, EventArgs e)
        {
            AnimateDoors("open");
            // Start timer to auto-close doors after 3 seconds
            doorCloseTimer.Start();
        }

        private void Elevator_DoorsClosed(object? sender, EventArgs e)
        {
            doorCloseTimer.Stop(); // Stop auto-close timer
            AnimateDoors("close");
        }

        private void Elevator_MovementStarted(object? sender, EventArgs e)
        {
            AnimateLift();
        }

        private void Elevator_MovementCompleted(object? sender, EventArgs e)
        {
            // Doors will open automatically
        }

        #endregion

        #region Form Load

        private void main_Load(object sender, EventArgs e)
        {
            // Initialize display labels
            lblFloor1Status.Text = "Floor 1";
            lblFloor2Status.Text = "             ";
            lblDisplayWindow.Text = "Floor 1";

            // Position elevator at Floor 1
            liftBox.Top = FLOOR1_Y;

            // Close doors initially
            leftDoor1.Left = FLOOR1_LEFT_DOOR_CLOSED;
            rightDoor1.Left = FLOOR1_RIGHT_DOOR_CLOSED;
            leftDoor2.Left = FLOOR2_LEFT_DOOR_CLOSED;
            rightDoor2.Left = FLOOR2_RIGHT_DOOR_CLOSED;

            // Set timer intervals for smooth animation
            doorTimer.Interval = 30;  // 30ms for smoother door animation
            liftTimer.Interval = 30;  // 30ms for smoother lift animation
        }

        #endregion

        #region Animation Methods

        /// <summary>
        /// Animate doors opening or closing
        /// </summary>
        private void AnimateDoors(string action)
        {
            doorAction = action;
            doorAnimating = true;
            doorTimer.Start();
        }

        /// <summary>
        /// Animate lift movement
        /// </summary>
        private void AnimateLift()
        {
            liftTargetY = elevator.TargetFloor == 1 ? FLOOR1_Y : FLOOR2_Y;
            liftAnimating = true;
            liftTimer.Start();
        }

        /// <summary>
        /// Update floor display labels
        /// </summary>
        private void UpdateFloorDisplay(int floor)
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

        #endregion

        #region Timer Events

        private void doorTimer_Tick(object sender, EventArgs e)
        {
            if (!doorAnimating) return;

            bool animationComplete = false;

            if (doorAction == "open")
            {
                // Open doors - move apart
                if (elevator.CurrentFloor == 1)
                {
                    // Floor 1 doors
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
                else // Floor 2
                {
                    // Floor 2 doors
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
                // Close doors - move together
                if (elevator.CurrentFloor == 1)
                {
                    // Floor 1 doors
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
                else // Floor 2
                {
                    // Floor 2 doors
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

        private void liftTimer_Tick(object sender, EventArgs e)
        {
            if (!liftAnimating) return;

            bool arrived = false;

            // Move elevator towards target floor
            if (liftBox.Top < liftTargetY)
            {
                // Moving down to Floor 1
                liftBox.Top += LIFT_SPEED;
                if (liftBox.Top >= liftTargetY)
                {
                    liftBox.Top = liftTargetY;
                    arrived = true;
                }
            }
            else if (liftBox.Top > liftTargetY)
            {
                // Moving up to Floor 2
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

                // Update elevator state - it has arrived
                elevator.UpdatePosition();
            }
        }

        #endregion

        #region Button Click Events - Request Buttons

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (elevator.CurrentFloor != 1)
            {
                elevator.RequestFloor(1);
            }
            else
            {
                // Already at floor 2, just open doors
                elevator.OpenDoors();
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (elevator.CurrentFloor != 2)
            {
                elevator.RequestFloor(2);
            }
            else
            {
                // Already at floor 1, just open doors
                elevator.OpenDoors();
            }
        }

        #endregion

        #region Button Click Events - Control Panel

        private void btnFloor1_Click(object sender, EventArgs e)
        {
            // Floor 1 button on control panel
            elevator.RequestFloor(1);
        }

        private void btnFloor2_Click(object sender, EventArgs e)
        {
            // Floor 2 button on control panel
            elevator.RequestFloor(2);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Open doors manually
            doorCloseTimer.Stop(); // Stop auto-close
            elevator.OpenDoors();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close doors manually
            doorCloseTimer.Stop(); // Stop auto-close
            elevator.CloseDoors();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            // Show log form
            ShowLogForm();
        }

        #endregion

        #region Log Display

        /// <summary>
        /// Display logs in a new form
        /// </summary>
        private void ShowLogForm()
        {
            try
            {
                // Get all logs from database
                DataSet ds = elevator.GetLogger().GetDatabaseHelper().GetAllLogs();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Create and show log form
                    LogForm logForm = new LogForm(ds.Tables[0]);
                    logForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No logs found!", "Elevator Logs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading logs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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