using System;

namespace Lift
{
    
    public class Elevator
    {
        public int CurrentFloor { get; private set; }
        public bool IsMoving { get; private set; }
        public bool DoorsOpen { get; private set; }
        public int TargetFloor { get; private set; }

        private ElevatorLogger logger;

        
        private FloorStateContext stateContext;

        
        public event EventHandler<int>? FloorChanged;
        public event EventHandler? DoorsOpened;
        public event EventHandler? DoorsClosed;
        public event EventHandler? MovementStarted;
        public event EventHandler? MovementCompleted;
        public event EventHandler<string>? ErrorOccurred;

       
        public Elevator()
        {
            try
            {
                CurrentFloor = 1;  
                IsMoving = false;
                DoorsOpen = false;
                TargetFloor = 1;
                logger = new ElevatorLogger();
                stateContext = new FloorStateContext(); 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Elevator initialization error: {ex.Message}");
                throw new InvalidOperationException("Failed to initialize elevator system", ex);
            }
        }

        
        public void RequestFloor(int floor)
        {
            try
            {
                
                if (!FloorStateFactory.IsValidFloor(floor))
                {
                    string errorMsg = $"Invalid floor number: {floor}";
                    ErrorOccurred?.Invoke(this, errorMsg);
                    throw new ArgumentException(errorMsg);
                }

                
                if (IsMoving)
                {
                    System.Diagnostics.Debug.WriteLine("Request ignored - elevator is moving");
                    return;
                }

               
                try
                {
                    logger.LogFloorRequest(floor, CurrentFloor);
                }
                catch (Exception logEx)
                {
                    System.Diagnostics.Debug.WriteLine($"Logging error: {logEx.Message}");
                    
                }

                if (CurrentFloor == floor)
                {
                   
                    if (!DoorsOpen)
                    {
                        OpenDoors();
                    }
                    return;
                }

                TargetFloor = floor;

               
                if (DoorsOpen)
                {
                    CloseDoors();
                }
                else
                {
                    
                    CloseDoors(); 
                }
            }
            catch (ArgumentException)
            {
                throw; 
            }
            catch (Exception ex)
            {
                string errorMsg = $"Error requesting floor {floor}: {ex.Message}";
                System.Diagnostics.Debug.WriteLine(errorMsg);
                ErrorOccurred?.Invoke(this, errorMsg);
                throw new InvalidOperationException(errorMsg, ex);
            }
        }

        public void OpenDoors()
        {
            try
            {
                if (!DoorsOpen && !IsMoving)
                {
                    DoorsOpen = true;

                   
                    try
                    {
                        logger.LogDoorOpen(CurrentFloor);
                    }
                    catch (Exception logEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Logging error: {logEx.Message}");
                    }

                    DoorsOpened?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Error opening doors: {ex.Message}";
                System.Diagnostics.Debug.WriteLine(errorMsg);
                ErrorOccurred?.Invoke(this, errorMsg);
            }
        }

     
        public void CloseDoors()
        {
            try
            {
                if (DoorsOpen)
                {
                    DoorsOpen = false;

                    // Log with exception handling
                    try
                    {
                        logger.LogDoorClose(CurrentFloor);
                    }
                    catch (Exception logEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Logging error: {logEx.Message}");
                    }

                    DoorsClosed?.Invoke(this, EventArgs.Empty);

                    // Start moving if target floor is different
                    if (TargetFloor != CurrentFloor)
                    {
                        StartMoving();
                    }
                }
                else
                {
                    // Doors already closed, just start moving
                    if (TargetFloor != CurrentFloor && !IsMoving)
                    {
                        StartMoving();
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Error closing doors: {ex.Message}";
                System.Diagnostics.Debug.WriteLine(errorMsg);
                ErrorOccurred?.Invoke(this, errorMsg);
            }
        }

   
        private void StartMoving()
        {
            try
            {
                IsMoving = true;
                MovementStarted?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                IsMoving = false;
                string errorMsg = $"Error starting movement: {ex.Message}";
                System.Diagnostics.Debug.WriteLine(errorMsg);
                ErrorOccurred?.Invoke(this, errorMsg);
            }
        }

     
        public void UpdatePosition()
        {
            try
            {
                if (!IsMoving) return;

                int fromFloor = CurrentFloor;

                // Update current floor
                CurrentFloor = TargetFloor;
                IsMoving = false;

                // STATE PATTERN: Dynamic state dispatching without if/switch
                try
                {
                    stateContext.ChangeFloor(CurrentFloor);
                }
                catch (Exception stateEx)
                {
                    System.Diagnostics.Debug.WriteLine($"State change error: {stateEx.Message}");
                    // Continue operation even if state change fails
                }

                
                try
                {
                    logger.LogMovement(fromFloor, CurrentFloor);
                }
                catch (Exception logEx)
                {
                    System.Diagnostics.Debug.WriteLine($"Logging error: {logEx.Message}");
                    // Continue operation even if logging fails
                }

                FloorChanged?.Invoke(this, CurrentFloor);
                MovementCompleted?.Invoke(this, EventArgs.Empty);

                // Open doors when arrived
                OpenDoors();
            }
            catch (Exception ex)
            {
                IsMoving = false;
                string errorMsg = $"Error updating position: {ex.Message}";
                System.Diagnostics.Debug.WriteLine(errorMsg);
                ErrorOccurred?.Invoke(this, errorMsg);
            }
        }

    
        public string GetStatus()
        {
            try
            {
                string floorName = stateContext.GetCurrentFloorName();

                if (IsMoving)
                    return $"Moving to Floor {TargetFloor}";
                else if (DoorsOpen)
                    return $"{floorName} - Doors Open";
                else
                    return $"{floorName} - Doors Closed";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting status: {ex.Message}");
                return $"Floor {CurrentFloor}";
            }
        }

  
        public ElevatorLogger GetLogger()
        {
            return logger;
        }

   
        public IFloorState GetCurrentState()
        {
            try
            {
                return stateContext.GetCurrentState();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting state: {ex.Message}");
                return null;
            }
        }
    }
}