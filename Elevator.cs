using System;

namespace Lift
{
    /// <summary>
    /// Represents the Elevator with its core functionality
    /// Follows Single Responsibility Principle - handles only elevator logic
    /// </summary>
    public class Elevator
    {
        // Properties
        public int CurrentFloor { get; private set; }
        public bool IsMoving { get; private set; }
        public bool DoorsOpen { get; private set; }
        public int TargetFloor { get; private set; }

        // Logger
        private ElevatorLogger logger;

        // Events for communication with GUI (Observer pattern)
        public event EventHandler<int>? FloorChanged;
        public event EventHandler? DoorsOpened;
        public event EventHandler? DoorsClosed;
        public event EventHandler? MovementStarted;
        public event EventHandler? MovementCompleted;

        // Constructor
        public Elevator()
        {
            CurrentFloor = 1;  // Start at ground floor
            IsMoving = false;
            DoorsOpen = false;
            TargetFloor = 1;
            logger = new ElevatorLogger();
        }

        /// <summary>
        /// Request elevator to move to a specific floor
        /// </summary>
        public void RequestFloor(int floor)
        {
            if (floor < 1 || floor > 2)
                throw new ArgumentException("Invalid floor number");

            // Log the request
            logger.LogFloorRequest(floor, CurrentFloor);

            if (CurrentFloor == floor)
            {
                // Already at the floor, just open doors
                if (!DoorsOpen)
                {
                    OpenDoors();
                }
                return;
            }

            TargetFloor = floor;

            // Close doors if open, then move
            if (DoorsOpen)
            {
                CloseDoors();
            }
            else
            {
                // Doors already closed, start moving immediately
                CloseDoors(); // This will trigger movement
            }
        }

        /// <summary>
        /// Open elevator doors
        /// </summary>
        public void OpenDoors()
        {
            if (!DoorsOpen && !IsMoving)
            {
                DoorsOpen = true;
                logger.LogDoorOpen(CurrentFloor);
                DoorsOpened?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Close elevator doors and start moving if needed
        /// </summary>
        public void CloseDoors()
        {
            if (DoorsOpen)
            {
                DoorsOpen = false;
                logger.LogDoorClose(CurrentFloor);
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

        /// <summary>
        /// Start elevator movement
        /// </summary>
        private void StartMoving()
        {
            IsMoving = true;
            MovementStarted?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Update elevator position (called by timer when animation completes)
        /// </summary>
        public void UpdatePosition()
        {
            if (!IsMoving) return;

            int fromFloor = CurrentFloor;

            // Update current floor
            CurrentFloor = TargetFloor;
            IsMoving = false;

            // Log the movement
            logger.LogMovement(fromFloor, CurrentFloor);

            FloorChanged?.Invoke(this, CurrentFloor);
            MovementCompleted?.Invoke(this, EventArgs.Empty);

            // Open doors when arrived
            OpenDoors();
        }

        /// <summary>
        /// Get current status as string
        /// </summary>
        public string GetStatus()
        {
            if (IsMoving)
                return $"Moving to Floor {TargetFloor}";
            else if (DoorsOpen)
                return $"Floor {CurrentFloor} - Doors Open";
            else
                return $"Floor {CurrentFloor} - Doors Closed";
        }

        /// <summary>
        /// Get logger for accessing database (for log display)
        /// </summary>
        public ElevatorLogger GetLogger()
        {
            return logger;
        }
    }
}