using System;
using System.Data;
using System.Windows.Forms;

namespace Lift
{
   
    public partial class LogForm : Form
    {
        private DataTable logData;
        private DatabaseHelper dbHelper;

        public LogForm(DataTable data)
        {
            InitializeComponent();
            logData = data;
            dbHelper = new DatabaseHelper();
            LoadLogs(logData);
        }

        private void InitializeComponent()
        {
            DataGridView dgvLogs = new DataGridView();
            Button btnClose = new Button();
            Button btnDelete = new Button();
            Button btnClearAll = new Button();
            Button btnRefresh = new Button();

            SuspendLayout();

           
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Location = new System.Drawing.Point(12, 12);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dgvLogs.RowHeadersWidth = 51;
            dgvLogs.Size = new System.Drawing.Size(776, 380);
            dgvLogs.TabIndex = 0;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.MultiSelect = false;

            
            btnDelete.Location = new System.Drawing.Point(12, 410);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(120, 35);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Delete Selected";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += BtnDelete_Click;

          
            btnClearAll.Location = new System.Drawing.Point(150, 410);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new System.Drawing.Size(120, 35);
            btnClearAll.TabIndex = 2;
            btnClearAll.Text = "Clear All Logs";
            btnClearAll.UseVisualStyleBackColor = true;
            btnClearAll.Click += BtnClearAll_Click;


            btnRefresh.Location = new System.Drawing.Point(288, 410);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(120, 35);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;

            btnClose.Location = new System.Drawing.Point(668, 410);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(120, 35);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += (s, e) => this.Close();

            
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 460);
            Controls.Add(btnClose);
            Controls.Add(btnRefresh);
            Controls.Add(btnClearAll);
            Controls.Add(btnDelete);
            Controls.Add(dgvLogs);
            Name = "LogForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Elevator Operation Logs";
            ResumeLayout(false);

            this.dgvLogs = dgvLogs;
        }

        private DataGridView dgvLogs;

        private void LoadLogs(DataTable data)
        {
            try
            {
                dgvLogs.DataSource = data;

                // Format columns
                if (dgvLogs.Columns.Contains("LogID"))
                    dgvLogs.Columns["LogID"].HeaderText = "Log ID";
                if (dgvLogs.Columns.Contains("Timestamp"))
                {
                    dgvLogs.Columns["Timestamp"].HeaderText = "Date & Time";
                    dgvLogs.Columns["Timestamp"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                }
                if (dgvLogs.Columns.Contains("Action"))
                    dgvLogs.Columns["Action"].HeaderText = "Action";
                if (dgvLogs.Columns.Contains("FromFloor"))
                    dgvLogs.Columns["FromFloor"].HeaderText = "From Floor";
                if (dgvLogs.Columns.Contains("ToFloor"))
                    dgvLogs.Columns["ToFloor"].HeaderText = "To Floor";
                if (dgvLogs.Columns.Contains("Status"))
                    dgvLogs.Columns["Status"].HeaderText = "Status";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading logs: {ex.Message}", "Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLogs.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a log to delete.", "No Selection",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var result = MessageBox.Show("Are you sure you want to delete this log entry?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int logId = Convert.ToInt32(dgvLogs.SelectedRows[0].Cells["LogID"].Value);

                    bool success = dbHelper.DeleteLog(logId);

                    if (success)
                    {
                        MessageBox.Show("Log deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshLogs();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete log.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting log: {ex.Message}", "Delete Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Are you sure you want to delete ALL logs? This cannot be undone!",
                    "Confirm Clear All", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool success = dbHelper.ClearAllLogs();

                    if (success)
                    {
                        MessageBox.Show("All logs cleared successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshLogs();
                    }
                    else
                    {
                        MessageBox.Show("Failed to clear logs.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing logs: {ex.Message}", "Clear Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshLogs();
                MessageBox.Show("Logs refreshed!", "Refresh",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing logs: {ex.Message}", "Refresh Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void RefreshLogs()
        {
            try
            {
                DataSet ds = dbHelper.GetAllLogs();
                if (ds.Tables.Count > 0)
                {
                    logData = ds.Tables[0];
                    dgvLogs.DataSource = logData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}", "Refresh Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}