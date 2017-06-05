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
    public partial class EditEmpInfo : Form, IParseEmpInfo
    {
        public string empFirst, empLast, empNSHE;

        public EditEmpInfo()
        {
            InitializeComponent();
            GetEmployeeInfo();
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

        public void GetEmployeeInfo(string infoFileName = "EmployeeInfo.txt")
        {
            string empFirst, empLast, empNSHE;
            Stack<string> empInfo = new Stack<string>();

            empFirst = File.ReadLines(infoFileName).Skip(0).Take(1).First();
            empFirst = empFirst.Split(')').Last().Trim();
            empInfo.Push(empFirst);

            empLast = File.ReadLines(infoFileName).Skip(1).Take(1).First();
            empLast = empLast.Split(')').Last().Trim();
            empInfo.Push(empLast);

            empNSHE = File.ReadLines(infoFileName).Skip(2).Take(1).First();
            empNSHE = empNSHE.Split(')').Last().Trim();
            empInfo.Push(empNSHE);

            textBoxNSHE.Text = empInfo.Pop();
            textBoxLName.Text = empInfo.Pop();
            textBoxFName.Text = empInfo.Pop();
            
        }
    }
}
