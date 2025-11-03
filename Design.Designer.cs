namespace Lift
{
    partial class Design
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Design));
            floor1 = new PictureBox();
            leftDoor1 = new PictureBox();
            rightDoor1 = new PictureBox();
            floor2 = new PictureBox();
            leftDoor2 = new PictureBox();
            rightDoor2 = new PictureBox();
            panel1 = new Panel();
            panel4 = new Panel();
            btnDown = new PictureBox();
            panel3 = new Panel();
            lblFloor1Status = new Label();
            lblFloor2Status = new Label();
            liftBox = new PictureBox();
            panel2 = new Panel();
            lblDisplayWindow = new Label();
            btnLog = new Button();
            btnClose = new PictureBox();
            btnOpen = new PictureBox();
            btnFloor2 = new PictureBox();
            btnFloor1 = new PictureBox();
            doorTimer = new System.Windows.Forms.Timer(components);
            liftTimer = new System.Windows.Forms.Timer(components);
            btnUp = new PictureBox();
            panel5 = new Panel();
            ((System.ComponentModel.ISupportInitialize)floor1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)leftDoor1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rightDoor1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)floor2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)leftDoor2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rightDoor2).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)liftBox).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnClose).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnOpen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnFloor2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnFloor1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnUp).BeginInit();
            SuspendLayout();
            // 
            // floor1
            // 
            floor1.Image = Properties.Resources.Screenshot_2025_11_02_193540;
            floor1.Location = new Point(176, 384);
            floor1.Name = "floor1";
            floor1.Size = new Size(193, 280);
            floor1.SizeMode = PictureBoxSizeMode.Zoom;
            floor1.TabIndex = 1;
            floor1.TabStop = false;
            // 
            // leftDoor1
            // 
            leftDoor1.Image = Properties.Resources.Screenshot_2025_11_02_203236;
            leftDoor1.Location = new Point(195, 405);
            leftDoor1.Name = "leftDoor1";
            leftDoor1.Size = new Size(77, 247);
            leftDoor1.SizeMode = PictureBoxSizeMode.Zoom;
            leftDoor1.TabIndex = 2;
            leftDoor1.TabStop = false;
            leftDoor1.Click += leftDoor1_Click;
            // 
            // rightDoor1
            // 
            rightDoor1.Image = Properties.Resources.Screenshot_2025_11_02_203236;
            rightDoor1.Location = new Point(271, 405);
            rightDoor1.Name = "rightDoor1";
            rightDoor1.Size = new Size(77, 247);
            rightDoor1.SizeMode = PictureBoxSizeMode.Zoom;
            rightDoor1.TabIndex = 3;
            rightDoor1.TabStop = false;
            rightDoor1.Click += rightDoor1_Click;
            // 
            // floor2
            // 
            floor2.Image = Properties.Resources.Screenshot_2025_11_02_193540;
            floor2.Location = new Point(264, 27);
            floor2.Name = "floor2";
            floor2.Size = new Size(193, 280);
            floor2.SizeMode = PictureBoxSizeMode.Zoom;
            floor2.TabIndex = 4;
            floor2.TabStop = false;
            floor2.Click += floor2_Click;
            // 
            // leftDoor2
            // 
            leftDoor2.Image = Properties.Resources.Screenshot_2025_11_02_203236;
            leftDoor2.Location = new Point(284, 48);
            leftDoor2.Name = "leftDoor2";
            leftDoor2.Size = new Size(78, 245);
            leftDoor2.SizeMode = PictureBoxSizeMode.Zoom;
            leftDoor2.TabIndex = 5;
            leftDoor2.TabStop = false;
            leftDoor2.Click += leftDoor2_Click;
            // 
            // rightDoor2
            // 
            rightDoor2.Image = Properties.Resources.Screenshot_2025_11_02_203236;
            rightDoor2.Location = new Point(359, 48);
            rightDoor2.Name = "rightDoor2";
            rightDoor2.Size = new Size(79, 245);
            rightDoor2.SizeMode = PictureBoxSizeMode.Zoom;
            rightDoor2.TabIndex = 6;
            rightDoor2.TabStop = false;
            rightDoor2.Click += rightDoor2_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightGray;
            panel1.Controls.Add(btnUp);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(btnDown);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(lblFloor1Status);
            panel1.Controls.Add(lblFloor2Status);
            panel1.Controls.Add(rightDoor1);
            panel1.Controls.Add(leftDoor1);
            panel1.Controls.Add(floor1);
            panel1.Controls.Add(liftBox);
            panel1.Location = new Point(88, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(547, 678);
            panel1.TabIndex = 7;
            // 
            // panel4
            // 
            panel4.Location = new Point(368, 384);
            panel4.Name = "panel4";
            panel4.Size = new Size(162, 280);
            panel4.TabIndex = 12;
            // 
            // btnDown
            // 
            btnDown.Image = Properties.Resources.Screenshot_2025_11_02_220708;
            btnDown.Location = new Point(69, 485);
            btnDown.Name = "btnDown";
            btnDown.Size = new Size(56, 61);
            btnDown.SizeMode = PictureBoxSizeMode.Zoom;
            btnDown.TabIndex = 8;
            btnDown.TabStop = false;
            btnDown.Click += btnDown_Click;
            // 
            // panel3
            // 
            panel3.Location = new Point(15, 384);
            panel3.Name = "panel3";
            panel3.Size = new Size(162, 280);
            panel3.TabIndex = 9;
            // 
            // lblFloor1Status
            // 
            lblFloor1Status.AutoSize = true;
            lblFloor1Status.BackColor = Color.Black;
            lblFloor1Status.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFloor1Status.ForeColor = Color.Red;
            lblFloor1Status.Location = new Point(236, 359);
            lblFloor1Status.Name = "lblFloor1Status";
            lblFloor1Status.Size = new Size(71, 23);
            lblFloor1Status.TabIndex = 11;
            lblFloor1Status.Text = "Floor 1 ";
            // 
            // lblFloor2Status
            // 
            lblFloor2Status.AutoSize = true;
            lblFloor2Status.BackColor = Color.Black;
            lblFloor2Status.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFloor2Status.ForeColor = Color.Red;
            lblFloor2Status.Location = new Point(242, 2);
            lblFloor2Status.Name = "lblFloor2Status";
            lblFloor2Status.Size = new Size(0, 23);
            lblFloor2Status.TabIndex = 11;
            // 
            // liftBox
            // 
            liftBox.Image = Properties.Resources.Screenshot_2025_11_02_205440;
            liftBox.Location = new Point(196, 405);
            liftBox.Name = "liftBox";
            liftBox.Size = new Size(154, 247);
            liftBox.SizeMode = PictureBoxSizeMode.Zoom;
            liftBox.TabIndex = 8;
            liftBox.TabStop = false;
            liftBox.Click += liftBox_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Silver;
            panel2.Controls.Add(lblDisplayWindow);
            panel2.Controls.Add(btnLog);
            panel2.Controls.Add(btnClose);
            panel2.Controls.Add(btnOpen);
            panel2.Controls.Add(btnFloor2);
            panel2.Controls.Add(btnFloor1);
            panel2.Location = new Point(693, 95);
            panel2.Name = "panel2";
            panel2.Size = new Size(166, 451);
            panel2.TabIndex = 8;
            // 
            // lblDisplayWindow
            // 
            lblDisplayWindow.AutoSize = true;
            lblDisplayWindow.BackColor = Color.Black;
            lblDisplayWindow.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDisplayWindow.ForeColor = Color.Red;
            lblDisplayWindow.Location = new Point(42, 110);
            lblDisplayWindow.Name = "lblDisplayWindow";
            lblDisplayWindow.Size = new Size(78, 28);
            lblDisplayWindow.TabIndex = 11;
            lblDisplayWindow.Text = "Floor 1";
            // 
            // btnLog
            // 
            btnLog.BackColor = Color.Silver;
            btnLog.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLog.Location = new Point(56, 354);
            btnLog.Name = "btnLog";
            btnLog.Size = new Size(53, 32);
            btnLog.TabIndex = 10;
            btnLog.Text = "Log";
            btnLog.UseVisualStyleBackColor = false;
            btnLog.Click += btnLog_Click;
            // 
            // btnClose
            // 
            btnClose.Image = Properties.Resources.Screenshot_2025_11_02_221207;
            btnClose.Location = new Point(98, 289);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(42, 45);
            btnClose.SizeMode = PictureBoxSizeMode.Zoom;
            btnClose.TabIndex = 9;
            btnClose.TabStop = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnOpen
            // 
            btnOpen.Image = Properties.Resources.Screenshot_2025_11_02_221156;
            btnOpen.Location = new Point(27, 289);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(42, 45);
            btnOpen.SizeMode = PictureBoxSizeMode.Zoom;
            btnOpen.TabIndex = 9;
            btnOpen.TabStop = false;
            btnOpen.Click += btnOpen_Click;
            // 
            // btnFloor2
            // 
            btnFloor2.Image = Properties.Resources.Screenshot_2025_11_02_2215432;
            btnFloor2.Location = new Point(98, 220);
            btnFloor2.Name = "btnFloor2";
            btnFloor2.Size = new Size(42, 43);
            btnFloor2.SizeMode = PictureBoxSizeMode.Zoom;
            btnFloor2.TabIndex = 9;
            btnFloor2.TabStop = false;
            btnFloor2.Click += btnFloor2_Click;
            // 
            // btnFloor1
            // 
            btnFloor1.Image = (Image)resources.GetObject("btnFloor1.Image");
            btnFloor1.Location = new Point(27, 220);
            btnFloor1.Name = "btnFloor1";
            btnFloor1.Size = new Size(42, 43);
            btnFloor1.SizeMode = PictureBoxSizeMode.Zoom;
            btnFloor1.TabIndex = 9;
            btnFloor1.TabStop = false;
            btnFloor1.Click += btnFloor1_Click;
            // 
            // doorTimer
            // 
            doorTimer.Interval = 20;
            doorTimer.Tick += doorTimer_Tick;
            // 
            // liftTimer
            // 
            liftTimer.Interval = 20;
            liftTimer.Tick += liftTimer_Tick;
            // 
            // btnUp
            // 
            btnUp.Image = Properties.Resources.Screenshot_2025_11_02_220650;
            btnUp.Location = new Point(69, 118);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(56, 61);
            btnUp.SizeMode = PictureBoxSizeMode.Zoom;
            btnUp.TabIndex = 8;
            btnUp.TabStop = false;
            btnUp.Click += btnUp_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Gray;
            panel5.Location = new Point(772, 118);
            panel5.Name = "panel5";
            panel5.Size = new Size(162, 280);
            panel5.TabIndex = 9;
            // 
            // Design
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1088, 678);
            Controls.Add(panel2);
            Controls.Add(rightDoor2);
            Controls.Add(leftDoor2);
            Controls.Add(floor2);
            Controls.Add(panel1);
            Name = "Design";
            Text = "Form1";
            Load += main_Load;
            ((System.ComponentModel.ISupportInitialize)floor1).EndInit();
            ((System.ComponentModel.ISupportInitialize)leftDoor1).EndInit();
            ((System.ComponentModel.ISupportInitialize)rightDoor1).EndInit();
            ((System.ComponentModel.ISupportInitialize)floor2).EndInit();
            ((System.ComponentModel.ISupportInitialize)leftDoor2).EndInit();
            ((System.ComponentModel.ISupportInitialize)rightDoor2).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)btnDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)liftBox).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)btnClose).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnOpen).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnFloor2).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnFloor1).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnUp).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox floor1;
        private PictureBox leftDoor1;
        private PictureBox rightDoor1;
        private PictureBox floor2;
        private PictureBox leftDoor2;
        private PictureBox rightDoor2;
        private Panel panel1;
        private PictureBox liftBox;
        private PictureBox btnDown;
        private Panel panel2;
        private PictureBox btnFloor1;
        private PictureBox btnClose;
        private PictureBox btnOpen;
        private PictureBox btnFloor2;
        private System.Windows.Forms.Timer doorTimer;
        private Button btnLog;
        private Label lblFloor2Status;
        private Label lblDisplayWindow;
        private System.Windows.Forms.Timer liftTimer;
        private Label lblFloor1Status;
        private Panel panel3;
        private Panel panel4;
        private PictureBox btnUp;
        private Panel panel5;
    }
}