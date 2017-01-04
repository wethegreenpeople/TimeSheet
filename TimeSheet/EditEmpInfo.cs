using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TimeSheet
{
    public partial class EditEmpInfo : Form
    {
        public EditEmpInfo()
        {
            InitializeComponent();
            ParseEmpInfo empInfo = new ParseEmpInfo();
            empInfo.GetEmployeeInfo();
            textBoxFName.Text = empInfo.empFirst;
            textBoxLName.Text = empInfo.empLast;
            textBoxNSHE.Text = empInfo.empNSHE;
        }

        private void textBoxFName_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string fName = textBoxFName.Text;
            string lName = textBoxLName.Text;
            string NSHE = textBoxNSHE.Text;
            string employeeFile = "First Name) " + fName + Environment.NewLine +
                "Last Name) " + lName + Environment.NewLine +
                "NSHE ID) " + NSHE;

            File.WriteAllText("EmployeeInfo.txt", employeeFile);

            this.Close();
        }
    }
}
