using System;
using System.Data;
using System.Windows.Forms;

namespace Lift
{
    public partial class LogForm : Form
    {
        public LogForm(DataTable logData)
        {
            InitializeComponent();
            LoadLogs(logData);
        }

        private void InitializeComponent()
        {
            DataGridView dgvLogs = new DataGridView();
            Button btnClose = new Button();

            SuspendLayout();

            // 
            // dgvLogs
            // 
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

            // 
            // btnClose
            // 
            btnClose.Location = new System.Drawing.Point(350, 410);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(100, 35);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += (s, e) => this.Close();

            // 
            // LogForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 460);
            Controls.Add(btnClose);
            Controls.Add(dgvLogs);
            Name = "LogForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Elevator Operation Logs";
            ResumeLayout(false);

            this.dgvLogs = dgvLogs;
        }

        private DataGridView dgvLogs;

        private void LoadLogs(DataTable logData)
        {
            dgvLogs.DataSource = logData;

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
    }
}