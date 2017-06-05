namespace TimeSheet
{
    partial class MainForm
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
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editHoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editEmployeeInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addHoursToCalendarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxPayRange = new System.Windows.Forms.GroupBox();
            this.radioButton31st = new System.Windows.Forms.RadioButton();
            this.radioButton30th = new System.Windows.Forms.RadioButton();
            this.radioButton15th = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.groupBoxPayRange.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(12, 119);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(233, 53);
            this.buttonGenerate.TabIndex = 2;
            this.buttonGenerate.Text = "Generate Timesheet";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(257, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editInfoToolStripMenuItem,
            this.addHoursToCalendarToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editInfoToolStripMenuItem
            // 
            this.editInfoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editHoursToolStripMenuItem,
            this.editEmployeeInfoToolStripMenuItem});
            this.editInfoToolStripMenuItem.Name = "editInfoToolStripMenuItem";
            this.editInfoToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.editInfoToolStripMenuItem.Text = "Edit Info";
            // 
            // editHoursToolStripMenuItem
            // 
            this.editHoursToolStripMenuItem.Name = "editHoursToolStripMenuItem";
            this.editHoursToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.editHoursToolStripMenuItem.Text = "Edit Hours";
            this.editHoursToolStripMenuItem.Click += new System.EventHandler(this.buttonEditHours_Click);
            // 
            // editEmployeeInfoToolStripMenuItem
            // 
            this.editEmployeeInfoToolStripMenuItem.Name = "editEmployeeInfoToolStripMenuItem";
            this.editEmployeeInfoToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.editEmployeeInfoToolStripMenuItem.Text = "Edit Employee Info";
            this.editEmployeeInfoToolStripMenuItem.Click += new System.EventHandler(this.buttonEditEmpInfo_Click);
            // 
            // addHoursToCalendarToolStripMenuItem
            // 
            this.addHoursToCalendarToolStripMenuItem.Name = "addHoursToCalendarToolStripMenuItem";
            this.addHoursToCalendarToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.addHoursToCalendarToolStripMenuItem.Text = "Add Hours to Calendar";
            this.addHoursToCalendarToolStripMenuItem.Click += new System.EventHandler(this.buttonAddToCalendar_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check For Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usageToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // usageToolStripMenuItem
            // 
            this.usageToolStripMenuItem.Name = "usageToolStripMenuItem";
            this.usageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.usageToolStripMenuItem.Text = "Usage";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // groupBoxPayRange
            // 
            this.groupBoxPayRange.Controls.Add(this.radioButton31st);
            this.groupBoxPayRange.Controls.Add(this.radioButton30th);
            this.groupBoxPayRange.Controls.Add(this.radioButton15th);
            this.groupBoxPayRange.Location = new System.Drawing.Point(12, 27);
            this.groupBoxPayRange.Name = "groupBoxPayRange";
            this.groupBoxPayRange.Size = new System.Drawing.Size(120, 86);
            this.groupBoxPayRange.TabIndex = 9;
            this.groupBoxPayRange.TabStop = false;
            this.groupBoxPayRange.Text = "Pay Range";
            // 
            // radioButton31st
            // 
            this.radioButton31st.AutoSize = true;
            this.radioButton31st.Location = new System.Drawing.Point(6, 65);
            this.radioButton31st.Name = "radioButton31st";
            this.radioButton31st.Size = new System.Drawing.Size(75, 17);
            this.radioButton31st.TabIndex = 2;
            this.radioButton31st.TabStop = true;
            this.radioButton31st.Text = "16th - 31st";
            this.radioButton31st.UseVisualStyleBackColor = true;
            // 
            // radioButton30th
            // 
            this.radioButton30th.AutoSize = true;
            this.radioButton30th.Location = new System.Drawing.Point(6, 42);
            this.radioButton30th.Name = "radioButton30th";
            this.radioButton30th.Size = new System.Drawing.Size(76, 17);
            this.radioButton30th.TabIndex = 1;
            this.radioButton30th.TabStop = true;
            this.radioButton30th.Text = "16th - 30th";
            this.radioButton30th.UseVisualStyleBackColor = true;
            // 
            // radioButton15th
            // 
            this.radioButton15th.AutoSize = true;
            this.radioButton15th.Location = new System.Drawing.Point(6, 19);
            this.radioButton15th.Name = "radioButton15th";
            this.radioButton15th.Size = new System.Drawing.Size(69, 17);
            this.radioButton15th.TabIndex = 0;
            this.radioButton15th.TabStop = true;
            this.radioButton15th.Text = "1st - 15th";
            this.radioButton15th.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(257, 174);
            this.Controls.Add(this.groupBoxPayRange);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Timesheet Generator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxPayRange.ResumeLayout(false);
            this.groupBoxPayRange.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editHoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editEmployeeInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem addHoursToCalendarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxPayRange;
        private System.Windows.Forms.RadioButton radioButton15th;
        private System.Windows.Forms.RadioButton radioButton31st;
        private System.Windows.Forms.RadioButton radioButton30th;
    }
}

