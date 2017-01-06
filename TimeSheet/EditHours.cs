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
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (string.IsNullOrWhiteSpace(textBox.Text) == true)
                    {
                        textBox.Text = "0";
                    }
                    // If not a total
                    if (textBox.TabIndex != 5 && textBox.TabIndex != 10 && textBox.TabIndex != 15 && textBox.TabIndex != 20 && textBox.TabIndex != 25)
                    {
                        // If not a day that you're not working
                        if (textBox.Text != "0")
                        {
                            if (textBox.Text.Contains("AM") == false && textBox.Text.Contains("PM") == false)
                            {
                                if (textBox.Text.Contains(':') == true)
                                {
                                    if (Convert.ToInt32(textBox.Text.Split(':').First().Trim()) > 6 && Convert.ToInt32(textBox.Text.Split(':').First().Trim()) <= 11)
                                    {
                                        textBox.Text = textBox.Text.Trim();
                                        textBox.Text += " AM";
                                    }
                                    if (Convert.ToInt32(textBox.Text.Split(':').First().Trim()) <= 5 || Convert.ToInt32(textBox.Text.Split(':').First().Trim()) == 12)
                                    {
                                        textBox.Text = textBox.Text.Trim();
                                        textBox.Text += " PM";
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(textBox.Text.Trim()) > 6 && Convert.ToInt32(textBox.Text.Trim()) <= 11)
                                    {
                                        textBox.Text = textBox.Text.Trim();
                                        textBox.Text += ":00 AM";
                                    }
                                    else if (Convert.ToInt32(textBox.Text.Trim()) <= 5 || Convert.ToInt32(textBox.Text.Trim()) == 12)
                                    {
                                        textBox.Text = textBox.Text.Trim();
                                        textBox.Text += ":00 PM";
                                    }
                                }
                            }
                        }
                    }
                    
                    if (textBox.TabIndex == 5 && (textBoxMonStart.Text == "0" || string.IsNullOrWhiteSpace(textBoxMonStart.Text) == true) && textBox.Text.Contains("8") == true)
                    {
                        textBoxMonStart.Text = "8:00 AM";
                        textBoxMonLunchOut.Text = "12:00 PM";
                        textBoxMonLunchIn.Text = "1:00 PM";
                        textBoxMonEnd.Text = "5:00 PM";
                    }
                    else if (textBox.TabIndex == 10 && (textBoxMonStart.Text == "0" || string.IsNullOrWhiteSpace(textBoxMonStart.Text) == true) && textBox.Text.Contains("8") == true)
                    {
                        textBoxTueStart.Text = "8:00 AM";
                        textBoxTueLunchOut.Text = "12:00 PM";
                        textBoxTueLunchIn.Text = "1:00 PM";
                        textBoxTueEnd.Text = "5:00 PM";
                    }
                    else if (textBox.TabIndex == 15 && (textBoxMonStart.Text == "0" || string.IsNullOrWhiteSpace(textBoxMonStart.Text) == true) && textBox.Text.Contains("8") == true)
                    {
                        textBoxWedStart.Text = "8:00 AM";
                        textBoxWedLunchOut.Text = "12:00 PM";
                        textBoxWedLunchIn.Text = "1:00 PM";
                        textBoxWedEnd.Text = "5:00 PM";
                    }
                    else if (textBox.TabIndex == 20 && (textBoxMonStart.Text == "0" || string.IsNullOrWhiteSpace(textBoxMonStart.Text) == true) && textBox.Text.Contains("8") == true)
                    {
                        textBoxThurStart.Text = "8:00 AM";
                        textBoxThurLunchOut.Text = "12:00 PM";
                        textBoxThurLunchIn.Text = "1:00 PM";
                        textBoxThurEnd.Text = "5:00 PM";
                    }
                    else if (textBox.TabIndex == 25 && (textBoxMonStart.Text == "0" || string.IsNullOrWhiteSpace(textBoxMonStart.Text) == true) && textBox.Text.Contains("8") == true)
                    {
                        textBoxFriStart.Text = "8:00 AM";
                        textBoxFriLunchOut.Text = "12:00 PM";
                        textBoxFriLunchIn.Text = "1:00 PM";
                        textBoxFriEnd.Text = "5:00 PM";
                    }
                }
            }

            ParseHours hours = new ParseHours();

            string hoursFile =
                "Day, Work Start, Lunch Out, Lunch In, Work End, Total Hours" + Environment.NewLine + Environment.NewLine +
                String.Format("Mon) {0}, {1}, {2}, {3}, {4}", textBoxMonStart.Text, textBoxMonLunchOut.Text, textBoxMonLunchIn.Text, textBoxMonEnd.Text, textBoxMonTotal.Text) + Environment.NewLine +
                String.Format("Tue) {0}, {1}, {2}, {3}, {4}", textBoxTueStart.Text, textBoxTueLunchOut.Text, textBoxTueLunchIn.Text, textBoxTueEnd.Text, textBoxTueTotal.Text) + Environment.NewLine +
                String.Format("Wed) {0}, {1}, {2}, {3}, {4}", textBoxWedStart.Text, textBoxWedLunchOut.Text, textBoxWedLunchIn.Text, textBoxWedEnd.Text, textBoxWedTotal.Text) + Environment.NewLine +
                String.Format("Thu) {0}, {1}, {2}, {3}, {4}", textBoxThurStart.Text, textBoxThurLunchOut.Text, textBoxThurLunchIn.Text, textBoxThurEnd.Text, textBoxThurTotal.Text) + Environment.NewLine +
                String.Format("Fri) {0}, {1}, {2}, {3}, {4}", textBoxFriStart.Text, textBoxFriLunchOut.Text, textBoxFriLunchIn.Text, textBoxFriEnd.Text, textBoxFriTotal.Text) + Environment.NewLine;

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
