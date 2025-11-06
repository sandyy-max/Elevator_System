using System;

namespace Lift
{
   
    public class ElevatorLogger
    {
        private DatabaseHelper dbHelper;

        public ElevatorLogger()
        {
            try
            {
                dbHelper = new DatabaseHelper();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Logger initialization error: {ex.Message}");
                throw new InvalidOperationException("Failed to initialize elevator logger", ex);
            }
        }

       
        public bool LogMovement(int fromFloor, int toFloor)
        {
            try
            {
                if (fromFloor < 1 || toFloor < 1)
                {
                    throw new ArgumentException("Invalid floor numbers");
                }

                string action = $"Move from Floor {fromFloor} to Floor {toFloor}";
                return dbHelper.InsertLog(action, fromFloor, toFloor, "Completed");
            }
            catch (ArgumentException argEx)
            {
                System.Diagnostics.Debug.WriteLine($"Validation error in LogMovement: {argEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error logging movement: {ex.Message}");
                return false;
            }
        }

        public bool LogDoorOpen(int currentFloor)
        {
            try
            {
                if (currentFloor < 1)
                {
                    throw new ArgumentException("Invalid floor number");
                }

                string action = $"Door Opened at Floor {currentFloor}";
                return dbHelper.InsertLog(action, currentFloor, currentFloor, "Door Open");
            }
            catch (ArgumentException argEx)
            {
                System.Diagnostics.Debug.WriteLine($"Validation error in LogDoorOpen: {argEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error logging door open: {ex.Message}");
                return false;
            }
        }

       
        public bool LogDoorClose(int currentFloor)
        {
            try
            {
                if (currentFloor < 1)
                {
                    throw new ArgumentException("Invalid floor number");
                }

                string action = $"Door Closed at Floor {currentFloor}";
                return dbHelper.InsertLog(action, currentFloor, currentFloor, "Door Closed");
            }
            catch (ArgumentException argEx)
            {
                System.Diagnostics.Debug.WriteLine($"Validation error in LogDoorClose: {argEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error logging door close: {ex.Message}");
                return false;
            }
        }

       
        public bool LogFloorRequest(int requestedFloor, int currentFloor)
        {
            try
            {
                if (requestedFloor < 1 || currentFloor < 1)
                {
                    throw new ArgumentException("Invalid floor numbers");
                }

                string action = $"Floor {requestedFloor} Requested";
                return dbHelper.InsertLog(action, currentFloor, requestedFloor, "Request");
            }
            catch (ArgumentException argEx)
            {
                System.Diagnostics.Debug.WriteLine($"Validation error in LogFloorRequest: {argEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error logging floor request: {ex.Message}");
                return false;
            }
        }

        public DatabaseHelper GetDatabaseHelper()
        {
            return dbHelper;
        }
    }
}