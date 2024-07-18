namespace KernGUI
{
    partial class Form_KernGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GB_Port = new System.Windows.Forms.GroupBox();
            this.CB_SD = new System.Windows.Forms.ComboBox();
            this.CB_Speed = new System.Windows.Forms.ComboBox();
            this.BT_Port_Close = new System.Windows.Forms.Button();
            this.BT_Port_Open = new System.Windows.Forms.Button();
            this.BT_Port_Rescan = new System.Windows.Forms.Button();
            this.CB_Port = new System.Windows.Forms.ComboBox();
            this.Timer_Port = new System.Windows.Forms.Timer(this.components);
            this.GB_Response = new System.Windows.Forms.GroupBox();
            this.TB_Response_Type_Weight = new System.Windows.Forms.TextBox();
            this.TB_Response_Text = new System.Windows.Forms.TextBox();
            this.GB_Install = new System.Windows.Forms.GroupBox();
            this.BT_Try = new System.Windows.Forms.Button();
            this.BT_Uninstall = new System.Windows.Forms.Button();
            this.BT_Install = new System.Windows.Forms.Button();
            this.GB_Platform = new System.Windows.Forms.GroupBox();
            this.TB_COMPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Platform = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GB_Port.SuspendLayout();
            this.GB_Response.SuspendLayout();
            this.GB_Install.SuspendLayout();
            this.GB_Platform.SuspendLayout();
            this.SuspendLayout();
            // 
            // GB_Port
            // 
            this.GB_Port.Controls.Add(this.CB_SD);
            this.GB_Port.Controls.Add(this.CB_Speed);
            this.GB_Port.Controls.Add(this.BT_Port_Close);
            this.GB_Port.Controls.Add(this.BT_Port_Open);
            this.GB_Port.Controls.Add(this.BT_Port_Rescan);
            this.GB_Port.Controls.Add(this.CB_Port);
            this.GB_Port.Location = new System.Drawing.Point(0, 84);
            this.GB_Port.Name = "GB_Port";
            this.GB_Port.Size = new System.Drawing.Size(250, 80);
            this.GB_Port.TabIndex = 0;
            this.GB_Port.TabStop = false;
            this.GB_Port.Text = "Port";
            // 
            // CB_SD
            // 
            this.CB_SD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_SD.FormattingEnabled = true;
            this.CB_SD.Items.AddRange(new object[] {
            "None",
            "AUPr",
            "rEcr"});
            this.CB_SD.Location = new System.Drawing.Point(168, 19);
            this.CB_SD.Name = "CB_SD";
            this.CB_SD.Size = new System.Drawing.Size(75, 21);
            this.CB_SD.TabIndex = 2;
            this.CB_SD.SelectedIndexChanged += new System.EventHandler(this.CB_SD_SelectedIndexChanged);
            // 
            // CB_Speed
            // 
            this.CB_Speed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Speed.FormattingEnabled = true;
            this.CB_Speed.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600",
            "1382400"});
            this.CB_Speed.Location = new System.Drawing.Point(87, 18);
            this.CB_Speed.Name = "CB_Speed";
            this.CB_Speed.Size = new System.Drawing.Size(75, 21);
            this.CB_Speed.TabIndex = 1;
            this.CB_Speed.SelectedIndexChanged += new System.EventHandler(this.CB_Speed_SelectedIndexChanged);
            // 
            // BT_Port_Close
            // 
            this.BT_Port_Close.Location = new System.Drawing.Point(168, 46);
            this.BT_Port_Close.Name = "BT_Port_Close";
            this.BT_Port_Close.Size = new System.Drawing.Size(75, 23);
            this.BT_Port_Close.TabIndex = 5;
            this.BT_Port_Close.Text = "&Close";
            this.BT_Port_Close.UseVisualStyleBackColor = true;
            this.BT_Port_Close.Click += new System.EventHandler(this.BT_Port_Close_Click);
            // 
            // BT_Port_Open
            // 
            this.BT_Port_Open.Location = new System.Drawing.Point(87, 46);
            this.BT_Port_Open.Name = "BT_Port_Open";
            this.BT_Port_Open.Size = new System.Drawing.Size(75, 23);
            this.BT_Port_Open.TabIndex = 4;
            this.BT_Port_Open.Text = "&Open";
            this.BT_Port_Open.UseVisualStyleBackColor = true;
            this.BT_Port_Open.Click += new System.EventHandler(this.BT_Port_Open_Click);
            // 
            // BT_Port_Rescan
            // 
            this.BT_Port_Rescan.Location = new System.Drawing.Point(6, 46);
            this.BT_Port_Rescan.Name = "BT_Port_Rescan";
            this.BT_Port_Rescan.Size = new System.Drawing.Size(75, 23);
            this.BT_Port_Rescan.TabIndex = 3;
            this.BT_Port_Rescan.Text = "&Rescan";
            this.BT_Port_Rescan.UseVisualStyleBackColor = true;
            this.BT_Port_Rescan.Click += new System.EventHandler(this.BT_Port_Rescan_Click);
            // 
            // CB_Port
            // 
            this.CB_Port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Port.FormattingEnabled = true;
            this.CB_Port.Location = new System.Drawing.Point(6, 19);
            this.CB_Port.Name = "CB_Port";
            this.CB_Port.Size = new System.Drawing.Size(75, 21);
            this.CB_Port.TabIndex = 0;
            // 
            // Timer_Port
            // 
            this.Timer_Port.Enabled = true;
            this.Timer_Port.Tick += new System.EventHandler(this.Timer_Port_Tick);
            // 
            // GB_Response
            // 
            this.GB_Response.Controls.Add(this.TB_Response_Type_Weight);
            this.GB_Response.Controls.Add(this.TB_Response_Text);
            this.GB_Response.Location = new System.Drawing.Point(256, 84);
            this.GB_Response.Name = "GB_Response";
            this.GB_Response.Size = new System.Drawing.Size(200, 80);
            this.GB_Response.TabIndex = 1;
            this.GB_Response.TabStop = false;
            this.GB_Response.Text = "Response";
            // 
            // TB_Response_Type_Weight
            // 
            this.TB_Response_Type_Weight.Location = new System.Drawing.Point(6, 51);
            this.TB_Response_Type_Weight.Name = "TB_Response_Type_Weight";
            this.TB_Response_Type_Weight.ReadOnly = true;
            this.TB_Response_Type_Weight.Size = new System.Drawing.Size(185, 20);
            this.TB_Response_Type_Weight.TabIndex = 1;
            // 
            // TB_Response_Text
            // 
            this.TB_Response_Text.Location = new System.Drawing.Point(6, 19);
            this.TB_Response_Text.Name = "TB_Response_Text";
            this.TB_Response_Text.ReadOnly = true;
            this.TB_Response_Text.Size = new System.Drawing.Size(185, 20);
            this.TB_Response_Text.TabIndex = 0;
            // 
            // GB_Install
            // 
            this.GB_Install.Controls.Add(this.BT_Try);
            this.GB_Install.Controls.Add(this.BT_Uninstall);
            this.GB_Install.Controls.Add(this.BT_Install);
            this.GB_Install.Location = new System.Drawing.Point(0, 167);
            this.GB_Install.Name = "GB_Install";
            this.GB_Install.Size = new System.Drawing.Size(456, 52);
            this.GB_Install.TabIndex = 2;
            this.GB_Install.TabStop = false;
            this.GB_Install.Text = "Install driver";
            // 
            // BT_Try
            // 
            this.BT_Try.Location = new System.Drawing.Point(168, 19);
            this.BT_Try.Name = "BT_Try";
            this.BT_Try.Size = new System.Drawing.Size(75, 23);
            this.BT_Try.TabIndex = 2;
            this.BT_Try.Text = "&Try COM";
            this.BT_Try.UseVisualStyleBackColor = true;
            this.BT_Try.Click += new System.EventHandler(this.BT_Try_Click);
            // 
            // BT_Uninstall
            // 
            this.BT_Uninstall.Location = new System.Drawing.Point(87, 19);
            this.BT_Uninstall.Name = "BT_Uninstall";
            this.BT_Uninstall.Size = new System.Drawing.Size(75, 23);
            this.BT_Uninstall.TabIndex = 1;
            this.BT_Uninstall.Text = "&Uninstall";
            this.BT_Uninstall.UseVisualStyleBackColor = true;
            this.BT_Uninstall.Click += new System.EventHandler(this.BT_Uninstall_Click);
            // 
            // BT_Install
            // 
            this.BT_Install.Location = new System.Drawing.Point(6, 19);
            this.BT_Install.Name = "BT_Install";
            this.BT_Install.Size = new System.Drawing.Size(75, 23);
            this.BT_Install.TabIndex = 0;
            this.BT_Install.Text = "&Install";
            this.BT_Install.UseVisualStyleBackColor = true;
            this.BT_Install.Click += new System.EventHandler(this.BT_Install_Click);
            // 
            // GB_Platform
            // 
            this.GB_Platform.Controls.Add(this.TB_COMPath);
            this.GB_Platform.Controls.Add(this.label2);
            this.GB_Platform.Controls.Add(this.TB_Platform);
            this.GB_Platform.Controls.Add(this.label1);
            this.GB_Platform.Location = new System.Drawing.Point(0, 1);
            this.GB_Platform.Name = "GB_Platform";
            this.GB_Platform.Size = new System.Drawing.Size(456, 77);
            this.GB_Platform.TabIndex = 3;
            this.GB_Platform.TabStop = false;
            this.GB_Platform.Text = "Platform data";
            // 
            // TB_COMPath
            // 
            this.TB_COMPath.Location = new System.Drawing.Point(69, 45);
            this.TB_COMPath.Name = "TB_COMPath";
            this.TB_COMPath.ReadOnly = true;
            this.TB_COMPath.Size = new System.Drawing.Size(378, 20);
            this.TB_COMPath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "COM Name";
            // 
            // TB_Platform
            // 
            this.TB_Platform.Location = new System.Drawing.Point(69, 19);
            this.TB_Platform.Name = "TB_Platform";
            this.TB_Platform.ReadOnly = true;
            this.TB_Platform.Size = new System.Drawing.Size(40, 20);
            this.TB_Platform.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Platform";
            // 
            // Form_KernGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 221);
            this.Controls.Add(this.GB_Platform);
            this.Controls.Add(this.GB_Install);
            this.Controls.Add(this.GB_Response);
            this.Controls.Add(this.GB_Port);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form_KernGUI";
            this.Text = "KernGUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_KernGUI_FormClosing);
            this.Load += new System.EventHandler(this.Form_KernGUI_Load);
            this.GB_Port.ResumeLayout(false);
            this.GB_Response.ResumeLayout(false);
            this.GB_Response.PerformLayout();
            this.GB_Install.ResumeLayout(false);
            this.GB_Platform.ResumeLayout(false);
            this.GB_Platform.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GB_Port;
        private System.Windows.Forms.ComboBox CB_Port;
        private System.Windows.Forms.Button BT_Port_Open;
        private System.Windows.Forms.Button BT_Port_Rescan;
        private System.Windows.Forms.Button BT_Port_Close;
        private System.Windows.Forms.Timer Timer_Port;
        private System.Windows.Forms.ComboBox CB_Speed;
        private System.Windows.Forms.GroupBox GB_Response;
        private System.Windows.Forms.TextBox TB_Response_Type_Weight;
        private System.Windows.Forms.TextBox TB_Response_Text;
        private System.Windows.Forms.GroupBox GB_Install;
        private System.Windows.Forms.Button BT_Uninstall;
        private System.Windows.Forms.Button BT_Install;
        private System.Windows.Forms.Button BT_Try;
        private System.Windows.Forms.ComboBox CB_SD;
        private System.Windows.Forms.GroupBox GB_Platform;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_COMPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Platform;
    }
}

