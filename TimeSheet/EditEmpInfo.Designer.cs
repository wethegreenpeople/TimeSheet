namespace TimeSheet
{
    partial class EditEmpInfo
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
            this.labelFirstName = new System.Windows.Forms.Label();
            this.labelLastName = new System.Windows.Forms.Label();
            this.labelNSHEID = new System.Windows.Forms.Label();
            this.textBoxFName = new System.Windows.Forms.TextBox();
            this.textBoxNSHE = new System.Windows.Forms.TextBox();
            this.textBoxLName = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelFirstName
            // 
            this.labelFirstName.AutoSize = true;
            this.labelFirstName.Location = new System.Drawing.Point(12, 31);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(63, 13);
            this.labelFirstName.TabIndex = 0;
            this.labelFirstName.Text = "First Name: ";
            // 
            // labelLastName
            // 
            this.labelLastName.AutoSize = true;
            this.labelLastName.Location = new System.Drawing.Point(11, 57);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(64, 13);
            this.labelLastName.TabIndex = 1;
            this.labelLastName.Text = "Last Name: ";
            // 
            // labelNSHEID
            // 
            this.labelNSHEID.AutoSize = true;
            this.labelNSHEID.Location = new System.Drawing.Point(18, 83);
            this.labelNSHEID.Name = "labelNSHEID";
            this.labelNSHEID.Size = new System.Drawing.Size(57, 13);
            this.labelNSHEID.TabIndex = 2;
            this.labelNSHEID.Text = "NSHE ID: ";
            // 
            // textBoxFName
            // 
            this.textBoxFName.Location = new System.Drawing.Point(81, 28);
            this.textBoxFName.Name = "textBoxFName";
            this.textBoxFName.Size = new System.Drawing.Size(191, 20);
            this.textBoxFName.TabIndex = 1;
            this.textBoxFName.TextChanged += new System.EventHandler(this.textBoxFName_TextChanged);
            // 
            // textBoxNSHE
            // 
            this.textBoxNSHE.Location = new System.Drawing.Point(81, 80);
            this.textBoxNSHE.Name = "textBoxNSHE";
            this.textBoxNSHE.Size = new System.Drawing.Size(191, 20);
            this.textBoxNSHE.TabIndex = 3;
            // 
            // textBoxLName
            // 
            this.textBoxLName.Location = new System.Drawing.Point(81, 54);
            this.textBoxLName.Name = "textBoxLName";
            this.textBoxLName.Size = new System.Drawing.Size(191, 20);
            this.textBoxLName.TabIndex = 2;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(99, 118);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // EditEmpInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 142);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxLName);
            this.Controls.Add(this.textBoxNSHE);
            this.Controls.Add(this.textBoxFName);
            this.Controls.Add(this.labelNSHEID);
            this.Controls.Add(this.labelLastName);
            this.Controls.Add(this.labelFirstName);
            this.Name = "EditEmpInfo";
            this.Text = "EditEmpInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.Label labelNSHEID;
        private System.Windows.Forms.TextBox textBoxFName;
        private System.Windows.Forms.TextBox textBoxNSHE;
        private System.Windows.Forms.TextBox textBoxLName;
        private System.Windows.Forms.Button buttonSave;

    }
}