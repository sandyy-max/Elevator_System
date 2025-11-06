using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Lift
{
    
    public class DatabaseHelper
    {
        private readonly string _connectionString =
            @"Server=DESKTOP-69H2049; Database=lift_log; 
              Trusted_Connection=True; TrustServerCertificate=True;";

   
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
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Connection Error: {sqlEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Connection test failed: {ex.Message}");
                return false;
            }
        }

      
        public bool InsertLog(string action, int fromFloor, int toFloor, string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(action))
                {
                    throw new ArgumentException("Action cannot be empty");
                }

                if (string.IsNullOrWhiteSpace(status))
                {
                    throw new ArgumentException("Status cannot be empty");
                }

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
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Insert Error: {sqlEx.Message}");
                return false;
            }
            catch (ArgumentException argEx)
            {
                System.Diagnostics.Debug.WriteLine($"Validation Error: {argEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Insert log failed: {ex.Message}");
                return false;
            }
        }

 
        public DataSet GetAllLogs()
        {
            DataSet ds = new DataSet();
            try
            {
                string query = @"SELECT LogID, Timestamp, Action, FromFloor, ToFloor, Status 
                                 FROM ElevatorLogs 
                                 ORDER BY Timestamp DESC;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    adapter.Fill(ds);
                }

                return ds;
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Select Error: {sqlEx.Message}");
                return ds; 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Get logs failed: {ex.Message}");
                return ds; 
            }
        }

      
        public bool DeleteLog(int logId)
        {
            try
            {
                if (logId <= 0)
                {
                    throw new ArgumentException("Invalid log ID");
                }

                string query = "DELETE FROM ElevatorLogs WHERE LogID = @LogID;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LogID", logId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Delete Error: {sqlEx.Message}");
                return false;
            }
            catch (ArgumentException argEx)
            {
                System.Diagnostics.Debug.WriteLine($"Validation Error: {argEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Delete log failed: {ex.Message}");
                return false;
            }
        }

  
        public bool ClearAllLogs()
        {
            try
            {
                string query = "DELETE FROM ElevatorLogs;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Clear Error: {sqlEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Clear logs failed: {ex.Message}");
                return false;
            }
        }

        public bool UpdateLogsWithDataAdapter(DataSet dataSet)
        {
            try
            {
                if (dataSet == null || dataSet.Tables.Count == 0)
                {
                    throw new ArgumentException("Dataset is empty");
                }

                string query = "SELECT * FROM ElevatorLogs";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.UpdateCommand = builder.GetUpdateCommand();
                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.DeleteCommand = builder.GetDeleteCommand();

                    int rowsAffected = adapter.Update(dataSet.Tables[0]);
                    return rowsAffected > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Update Error: {sqlEx.Message}");
                return false;
            }
            catch (ArgumentException argEx)
            {
                System.Diagnostics.Debug.WriteLine($"Validation Error: {argEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Update with DataAdapter failed: {ex.Message}");
                return false;
            }
        }

      
        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}