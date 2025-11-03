using System;

namespace Lift
{
    /// <summary>
    /// Handles logging of elevator operations
    /// Follows Single Responsibility Principle
    /// </summary>
    public class ElevatorLogger
    {
        private DatabaseHelper dbHelper;

        public ElevatorLogger()
        {
            dbHelper = new DatabaseHelper();
        }

        /// <summary>
        /// Log elevator movement
        /// </summary>
        public void LogMovement(int fromFloor, int toFloor)
        {
            string action = $"Move from Floor {fromFloor} to Floor {toFloor}";
            dbHelper.InsertLog(action, fromFloor, toFloor, "Completed");
        }

        /// <summary>
        /// Log door opening
        /// </summary>
        public void LogDoorOpen(int currentFloor)
        {
            string action = $"Door Opened at Floor {currentFloor}";
            dbHelper.InsertLog(action, currentFloor, currentFloor, "Door Open");
        }

        /// <summary>
        /// Log door closing
        /// </summary>
        public void LogDoorClose(int currentFloor)
        {
            string action = $"Door Closed at Floor {currentFloor}";
            dbHelper.InsertLog(action, currentFloor, currentFloor, "Door Closed");
        }

        /// <summary>
        /// Log floor request
        /// </summary>
        public void LogFloorRequest(int requestedFloor, int currentFloor)
        {
            string action = $"Floor {requestedFloor} Requested";
            dbHelper.InsertLog(action, currentFloor, requestedFloor, "Request");
        }

        /// <summary>
        /// Get database helper for direct access
        /// </summary>
        public DatabaseHelper GetDatabaseHelper()
        {
            return dbHelper;
        }
    }
}