using System;
using System.Data;

using Microsoft.Data.SqlClient;

namespace Lift
{
    /// <summary>
    /// Handles all database operations using SQL Server
    /// Follows Single Responsibility Principle - only database logic
    /// Uses Disconnected Model with DataAdapter (Task 3 requirement)
    /// </summary>
    public class DatabaseHelper
    {
        private readonly string _connectionString =
            @"Server=DESKTOP-69H2049; Database=lift_log; 
              Trusted_Connection=True; TrustServerCertificate=True;";

        /// <summary>
        /// Insert a log entry into database
        /// </summary>
        public bool InsertLog(string action, int fromFloor, int toFloor, string status)
        {
            try
            {
                string query = @"INSERT INTO ElevatorLogs (Timestamp, Action, FromFloor, ToFloor, Status)
                                 VALUES (@Timestamp, @Action, @FromFloor, @ToFloor, @Status);";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Action", action);
                    cmd.Parameters.AddWithValue("@FromFloor", fromFloor);
                    cmd.Parameters.AddWithValue("@ToFloor", toFloor);
                    cmd.Parameters.AddWithValue("@Status", status);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Insert log failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Get all logs from database using DataAdapter (Disconnected Model)
        /// Task 3 requirement: Use DataAdapter
        /// </summary>
        public DataSet GetAllLogs()
        {
            try
            {
                string query = @"SELECT LogID, Timestamp, Action, FromFloor, ToFloor, Status 
                                 FROM ElevatorLogs 
                                 ORDER BY Timestamp DESC;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Get logs failed: {ex.Message}");
                return new DataSet();
            }
        }

        /// <summary>
        /// Test database connection
        /// </summary>
        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Connection test failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Clear all logs (optional - for testing)
        /// </summary>
        public bool ClearAllLogs()
        {
            try
            {
                string query = "DELETE FROM ElevatorLogs;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery() >= 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Clear logs failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Update logs using DataAdapter.Update() method (Task 3 requirement)
        /// This demonstrates the disconnected model for updating
        /// </summary>
        public bool UpdateLogsWithDataAdapter(DataSet dataSet)
        {
            try
            {
                string query = "SELECT * FROM ElevatorLogs";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.UpdateCommand = builder.GetUpdateCommand();
                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.DeleteCommand = builder.GetDeleteCommand();

                    return adapter.Update(dataSet.Tables[0]) > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Update with DataAdapter failed: {ex.Message}");
                return false;
            }
        }
    }
}