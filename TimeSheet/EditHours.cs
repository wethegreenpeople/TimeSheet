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
    public partial class EditHours : Form
    {
        public EditHours()
        {
            InitializeComponent();

            ParseHours hours = new ParseHours();
            hours.HoursWorked();

            textBoxMonStart.Text = hours.mondayStart.Trim();
            textBoxMonEnd.Text = hours.mondayEnd.Trim();
            textBoxMonLunchIn.Text = hours.mondayBreakEnd.Trim();
            textBoxMonLunchOut.Text = hours.mondayBreakStart.Trim();
            textBoxMonTotal.Text = hours.mondayTotal.Trim();

            textBoxTueStart.Text = hours.tuesdayStart.Trim();
            textBoxTueEnd.Text = hours.tuesdayEnd.Trim();
            textBoxTueLunchIn.Text = hours.tuesdayBreakEnd.Trim();
            textBoxTueLunchOut.Text = hours.tuesdayBreakStart.Trim();
            textBoxTueTotal.Text = hours.tuesdayTotal.Trim();

            textBoxWedStart.Text = hours.wednesdayStart.Trim();
            textBoxWedEnd.Text = hours.wednesdayEnd.Trim();
            textBoxWedLunchIn.Text = hours.wednesdayBreakEnd.Trim();
            textBoxWedLunchOut.Text = hours.wednesdayBreakStart.Trim();
            textBoxWedTotal.Text = hours.wednesdayTotal.Trim();

            textBoxThurStart.Text = hours.thursdayStart.Trim();
            textBoxThurEnd.Text = hours.thursdayEnd.Trim();
            textBoxThurLunchIn.Text = hours.thursdayBreakEnd.Trim();
            textBoxThurLunchOut.Text = hours.thursdayBreakStart.Trim();
            textBoxThurTotal.Text = hours.thursdayTotal.Trim();

            textBoxFriStart.Text = hours.fridayStart.Trim();
            textBoxFriEnd.Text = hours.fridayEnd.Trim();
            textBoxFriLunchIn.Text = hours.fridayBreakEnd.Trim();
            textBoxFriLunchOut.Text = hours.fridayBreakStart.Trim();
            textBoxFriTotal.Text = hours.fridayTotal.Trim();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string monStart = textBoxMonStart.Text;
            string monEnd = textBoxMonEnd.Text;
            string monLunchIn = textBoxMonLunchIn.Text;
            string monLunchOut = textBoxMonLunchOut.Text;
            string monTotal = textBoxMonTotal.Text;

            string tueStart = textBoxTueStart.Text;
            string tueEnd = textBoxTueEnd.Text;
            string tueLunchIn = textBoxTueLunchIn.Text;
            string tueLunchOut = textBoxTueLunchOut.Text;
            string tueTotal = textBoxTueTotal.Text;

            string wedStart = textBoxWedStart.Text;
            string wedEnd = textBoxWedEnd.Text;
            string wedLunchIn = textBoxWedLunchIn.Text;
            string wedLunchOut = textBoxWedLunchOut.Text;
            string wedTotal = textBoxWedTotal.Text;

            string thurStart = textBoxThurStart.Text;
            string thurEnd = textBoxThurEnd.Text;
            string thurLunchIn = textBoxThurLunchIn.Text;
            string thurLunchOut = textBoxThurLunchOut.Text;
            string thurTotal = textBoxThurTotal.Text;

            string friStart = textBoxFriStart.Text;
            string friEnd = textBoxFriEnd.Text;
            string friLunchIn = textBoxFriLunchIn.Text;
            string friLunchOut = textBoxFriLunchOut.Text;
            string friTotal = textBoxFriTotal.Text;

            string hoursFile =
                "Day, Work Start, Lunch Out, Lunch In, Work End, Total Hours" + Environment.NewLine + Environment.NewLine +
                String.Format("Mon) {0}, {1}, {2}, {3}, {4}", monStart, monLunchOut, monLunchIn, monEnd, monTotal) + Environment.NewLine +
                String.Format("Tue) {0}, {1}, {2}, {3}, {4}", tueStart, tueLunchOut, tueLunchIn, tueEnd, tueTotal) + Environment.NewLine +
                String.Format("Wed) {0}, {1}, {2}, {3}, {4}", wedStart, wedLunchOut, wedLunchIn, wedEnd, wedTotal) + Environment.NewLine +
                String.Format("Thu) {0}, {1}, {2}, {3}, {4}", thurStart, thurLunchOut, thurLunchIn, thurEnd, thurTotal) + Environment.NewLine +
                String.Format("Fri) {0}, {1}, {2}, {3}, {4}", friStart, friLunchOut, friLunchIn, friEnd, friTotal) + Environment.NewLine;

            File.WriteAllText("hours.txt", hoursFile);

            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxWedTotal_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
