using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text.pdf;
using System.Diagnostics;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Net;
using HtmlAgilityPack;
using AutoUpdaterDotNET;
using System.Net;

namespace TimeSheet
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UpdateProgram.CheckForUpdates("newestversion.ini");
            // If running right after an update, make sure we're renaming the file
            if (System.AppDomain.CurrentDomain.FriendlyName == "Update.exe")
            {
                File.Delete("TimeSheet.exe");
                File.Move("Update.exe", "TimeSheet.exe");
            }

            // Making sure that the required files are available.
            string hoursFile = "Day, Work Start, Lunch Out, Lunch In, Work End, Total Hours" + Environment.NewLine + Environment.NewLine +
                "Mon) 0, 0, 0, 0, 0" + Environment.NewLine +
                "Tue) 0, 0, 0, 0, 0" + Environment.NewLine +
                "Wed) 0, 0, 0, 0, 0" + Environment.NewLine +
                "Thu) 0, 0, 0, 0, 0" + Environment.NewLine +
                "Fri) 0, 0, 0, 0, 0";
            string employeeFile = "First Name)" + Environment.NewLine +
                "Last Name)" + Environment.NewLine +
                "NSHE ID)";
            if (File.Exists("hours.txt") == false)
            {
                File.WriteAllText("hours.txt", hoursFile);
            }
            if (File.Exists("EmployeeInfo.txt") == false)
            {
                File.WriteAllText("EmployeeInfo.txt", employeeFile);
            }
            if (File.Exists("time.pdf") == false)
            {
                WebClient client = new WebClient();
                client.DownloadFile("http://uraqt.xyz/uselessprograms/time.pdf", "time.pdf"); // Downloading the .pdf from uraqt.xyz
            }
        }

        public void ReplacePdfForm()
        {
            DateTime dateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            string fileNameExisting = "time.pdf";
            string fileNameNew = "TimeSheet" + dateValue.Month + dateValue.Year + ".pdf";

            double hoursTotalInt = 0;

            ParseHours hours = new ParseHours();
            hours.HoursWorked();

            // Employee Info Variables
            string empFirst = File.ReadLines("EmployeeInfo.txt").Skip(0).Take(1).First();
            empFirst = empFirst.Split(')').Last();
            string empLast = File.ReadLines("EmployeeInfo.txt").Skip(1).Take(1).First();
            empLast = empLast.Split(')').Last();
            string empNSHE = File.ReadLines("EmployeeInfo.txt").Skip(2).Take(1).First();
            empNSHE = empNSHE.Split(')').Last();

            using (var existingFileStream = new FileStream(fileNameExisting, FileMode.Open))
            using (var newFileStream = new FileStream(fileNameNew, FileMode.Create))
            {
                // Open existing PDF
                var pdfReader = new PdfReader(existingFileStream);

                // PdfStamper, which will create
                var stamper = new PdfStamper(pdfReader, newFileStream, '\0', true);

                var form = stamper.AcroFields;
                var fieldKeys = form.Fields.Keys;

                if (radioButton15th.Checked)
                {
                    for (int i = 1; i < 16; ++i)
                    {
                        // We're building a date: DD/MM/YYYY
                        // fieldKey is the name of the textbox where the date is being entered
                        // Then we set it
                        string dateText = string.Format("{0}/{1}/{2}", DateTime.Now.Month, i, DateTime.Now.Year);
                        string fieldKey = string.Format("DateRow{0}", i);
                        form.SetField(fieldKey, dateText);
                    }
                }
                else if (radioButton30th.Checked)
                {
                    for (int i = 1; i < 16; ++i)
                    {
                        string dateText = string.Format("{0}/{1}/{2}", DateTime.Now.Month, i + 15, DateTime.Now.Year);
                        string fieldKey = string.Format("DateRow{0}", i);
                        form.SetField(fieldKey, dateText);
                    }
                }
                else if (radioButton31st.Checked)
                {
                    for (int i = 1; i < 16; ++i)
                    {
                        string dateText = string.Format("{0}/{1}/{2}", DateTime.Now.Month, i + 15, DateTime.Now.Year);
                        string fieldKey = string.Format("DateRow{0}", i);
                        form.SetField(fieldKey, dateText);
                    }
                    string extraDateText = string.Format("{0}/31/{1}", DateTime.Now.Month, DateTime.Now.Year);
                    form.SetField("DateRow16", extraDateText);
                }

                int x = 16;
                // Adding working in hours
                try
                {
                    foreach (string fieldKey in fieldKeys)
                    {
                        for (int i = 1; i < x; ++i)
                        {
                            // 1st - 15th
                            if (radioButton15th.Checked)
                            {
                                if (fieldKey.Equals("Work StartRow" + i))
                                {
                                    switch (dateValue.AddDays(i - 1).ToString("ddd"))
                                    {
                                        case "Mon":
                                            form.SetField(fieldKey, hours.mondayStart);
                                            form.SetField("Work EndRow" + i, hours.mondayEnd);
                                            form.SetField("Break OutRow" + i, hours.mondayBreakStart);
                                            form.SetField("Break InRow" + i, hours.mondayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.mondayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.mondayTotal);
                                            break;
                                        case "Tue":
                                            form.SetField(fieldKey, hours.tuesdayStart);
                                            form.SetField("Work EndRow" + i, hours.tuesdayEnd);
                                            form.SetField("Break OutRow" + i, hours.tuesdayBreakStart);
                                            form.SetField("Break InRow" + i, hours.tuesdayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.tuesdayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.tuesdayTotal);
                                            break;
                                        case "Wed":
                                            form.SetField(fieldKey, hours.wednesdayStart);
                                            form.SetField("Work EndRow" + i, hours.wednesdayEnd);
                                            form.SetField("Break OutRow" + i, hours.wednesdayBreakStart);
                                            form.SetField("Break InRow" + i, hours.wednesdayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.wednesdayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.wednesdayTotal);
                                            break;
                                        case "Thu":
                                            form.SetField(fieldKey, hours.thursdayStart);
                                            form.SetField("Work EndRow" + i, hours.thursdayEnd);
                                            form.SetField("Break OutRow" + i, hours.thursdayBreakStart);
                                            form.SetField("Break InRow" + i, hours.thursdayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.thursdayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.thursdayTotal);
                                            break;
                                        case "Fri":
                                            form.SetField(fieldKey, hours.fridayStart);
                                            form.SetField("Work EndRow" + i, hours.fridayEnd);
                                            form.SetField("Break OutRow" + i, hours.fridayBreakStart);
                                            form.SetField("Break InRow" + i, hours.fridayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.fridayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.fridayTotal);
                                            break;
                                        default:

                                            break;
                                    } // Switch
                                }
                            } // if 1st - 15th

                            //16th - 30th
                            else if (radioButton30th.Checked)
                            {
                                if (fieldKey.Equals("Work StartRow" + i))
                                {
                                    switch (dateValue.AddDays(i + 15 - 1).ToString("ddd"))
                                    {
                                        case "Mon":
                                            form.SetField(fieldKey, hours.mondayStart);
                                            form.SetField("Work EndRow" + i, hours.mondayEnd);
                                            form.SetField("Break OutRow" + i, hours.mondayBreakStart);
                                            form.SetField("Break InRow" + i, hours.mondayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.mondayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.mondayTotal);
                                            break;
                                        case "Tue":
                                            form.SetField(fieldKey, hours.tuesdayStart);
                                            form.SetField("Work EndRow" + i, hours.tuesdayEnd);
                                            form.SetField("Break OutRow" + i, hours.tuesdayBreakStart);
                                            form.SetField("Break InRow" + i, hours.tuesdayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.tuesdayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.tuesdayTotal);
                                            break;
                                        case "Wed":
                                            form.SetField(fieldKey, hours.wednesdayStart);
                                            form.SetField("Work EndRow" + i, hours.wednesdayEnd);
                                            form.SetField("Break OutRow" + i, hours.wednesdayBreakStart);
                                            form.SetField("Break InRow" + i, hours.wednesdayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.wednesdayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.wednesdayTotal);
                                            break;
                                        case "Thu":
                                            form.SetField(fieldKey, hours.thursdayStart);
                                            form.SetField("Work EndRow" + i, hours.thursdayEnd);
                                            form.SetField("Break OutRow" + i, hours.thursdayBreakStart);
                                            form.SetField("Break InRow" + i, hours.thursdayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.thursdayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.thursdayTotal);
                                            break;
                                        case "Fri":
                                            form.SetField(fieldKey, hours.fridayStart);
                                            form.SetField("Work EndRow" + i, hours.fridayEnd);
                                            form.SetField("Break OutRow" + i, hours.fridayBreakStart);
                                            form.SetField("Break InRow" + i, hours.fridayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.fridayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.fridayTotal);
                                            break;
                                        default:

                                            break;
                                    } // switch
                                }
                            } // if 16th - 30th

                            //16th - 31st
                            if (radioButton31st.Checked)
                            {
                                x = 17;
                                if (fieldKey.Equals("Work StartRow" + i))
                                {
                                    switch (dateValue.AddDays(i + 15 - 1).ToString("ddd"))
                                    {
                                        case "Mon":
                                            form.SetField(fieldKey, hours.mondayStart);
                                            form.SetField("Work EndRow" + i, hours.mondayEnd);
                                            form.SetField("Break OutRow" + i, hours.mondayBreakStart);
                                            form.SetField("Break InRow" + i, hours.mondayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.mondayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.mondayTotal);
                                            break;
                                        case "Tue":
                                            form.SetField(fieldKey, hours.tuesdayStart);
                                            form.SetField("Work EndRow" + i, hours.tuesdayEnd);
                                            form.SetField("Break OutRow" + i, hours.tuesdayBreakStart);
                                            form.SetField("Break InRow" + i, hours.tuesdayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.tuesdayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.tuesdayTotal);
                                            break;
                                        case "Wed":
                                            form.SetField(fieldKey, hours.wednesdayStart);
                                            form.SetField("Work EndRow" + i, hours.wednesdayEnd);
                                            form.SetField("Break OutRow" + i, hours.wednesdayBreakStart);
                                            form.SetField("Break InRow" + i, hours.wednesdayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.wednesdayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.wednesdayTotal);
                                            break;
                                        case "Thu":
                                            form.SetField(fieldKey, hours.thursdayStart);
                                            form.SetField("Work EndRow" + i, hours.thursdayEnd);
                                            form.SetField("Break OutRow" + i, hours.thursdayBreakStart);
                                            form.SetField("Break InRow" + i, hours.thursdayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.thursdayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.thursdayTotal);
                                            break;
                                        case "Fri":
                                            form.SetField(fieldKey, hours.fridayStart);
                                            form.SetField("Work EndRow" + i, hours.fridayEnd);
                                            form.SetField("Break OutRow" + i, hours.fridayBreakStart);
                                            form.SetField("Break InRow" + i, hours.fridayBreakEnd);
                                            form.SetField("HoursRow" + i, hours.fridayTotal);
                                            hoursTotalInt = hoursTotalInt + Double.Parse(hours.fridayTotal);
                                            break;
                                        default:

                                            break;
                                    } // switch
                                }
                            } // if 16th - 31st
                        }
                    } // foreach field
                } // try

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // Adding employee info into the PDF
                form.SetField("Last Name", empLast);
                form.SetField("First Name", empFirst);
                form.SetField("Employee ID", empNSHE);
                form.SetField("HoursTotal Hours", Convert.ToString(hoursTotalInt));

                if (radioButton15th.Checked)
                {
                    string fromDate = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
                    string toDate = DateTime.Now.Month + "/15/" + DateTime.Now.Year;
                    form.SetField("Pay Period From To", fromDate);
                    form.SetField("Pay Period To", toDate);
                }
                if (radioButton30th.Checked)
                {
                    string fromDate = DateTime.Now.Month + "/16/" + DateTime.Now.Year;
                    string toDate = DateTime.Now.Month + "/30/" + DateTime.Now.Year;
                    form.SetField("Pay Period From To", fromDate);
                    form.SetField("Pay Period To", toDate);
                }
                else if (radioButton31st.Checked)
                {
                    string fromDate = DateTime.Now.Month + "/16/" + DateTime.Now.Year;
                    string toDate = DateTime.Now.Month + "/31/" + DateTime.Now.Year;
                    form.SetField("Pay Period From To", fromDate);
                    form.SetField("Pay Period To", toDate);
                }

                stamper.Close();
                pdfReader.Close();
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            string mondayHours = File.ReadLines("hours.txt").Skip(2).Take(1).First();
            mondayHours = mondayHours.Split(')').Last();
            string tuesdayHours = File.ReadLines("hours.txt").Skip(3).Take(1).First();
            tuesdayHours = tuesdayHours.Split(')').Last();
            string wednesdayHours = File.ReadLines("hours.txt").Skip(4).Take(1).First();
            wednesdayHours = wednesdayHours.Split(')').Last();
            string thursdayHours = File.ReadLines("hours.txt").Skip(5).Take(1).First();
            thursdayHours = thursdayHours.Split(')').Last();
            string fridayHours = File.ReadLines("hours.txt").Skip(6).Take(1).First();
            fridayHours = fridayHours.Split(')').Last();

            DateTime dateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            string fileHeader = string.Format("{0}{1}", "Date, Work Start, Break Out, Break In, Work End, Hours", Environment.NewLine);
            string fileFooter = string.Format("{0}{1}", ", , , , Total Hours, 0", Environment.NewLine);

            if (radioButton15th.Checked || radioButton30th.Checked || radioButton31st.Checked)
            {
                ReplacePdfForm();
                MessageBox.Show("Timesheet generated!", "Success", MessageBoxButtons.OK);
            }
            // catching if user hasn't selected a date range
            else
            {
                MessageBox.Show("Please select a date range before trying to generate timesheet!", "Error", MessageBoxButtons.OK);
            }
        }

        // Editing hours
        private void buttonEditHours_Click(object sender, EventArgs e)
        {
            EditHours hours = new EditHours();

            hours.Show();
        }

        // Editing employee info
        private void buttonEditEmpInfo_Click(object sender, EventArgs e)
        {
            EditEmpInfo empInfo = new EditEmpInfo();

            empInfo.Show();
        }

        public string dateRange;
        
        // Calendar update
        private void buttonAddToCalendar_Click(object sender, EventArgs e)
        {
            CalendarUpdate calendar = new CalendarUpdate();

            if (File.Exists("client_secret.json") == false) // File needed to access google api
            {
                WebClient client = new WebClient();
                client.DownloadFile("http://uraqt.xyz/uselessprograms/client_secret.json", "client_secret.json"); // same
            }

            if (radioButton15th.Checked)
            {
                calendar.dateRange = "1st - 15th";
            }

            else if (radioButton30th.Checked)
            {
                calendar.dateRange = "16th - 30th";
            }
            else if (radioButton31st.Checked)
            {
                calendar.dateRange = "16th - 31st";
            }
            else
            {
                MessageBox.Show("Please select a date range before trying to update your calendar", "Error", MessageBoxButtons.OK);
            }
            
            ParseHours hours = new ParseHours();
            hours.HoursWorked();
            CheckIfValidTime check = new CheckIfValidTime();
            int count = 0;
            // k. so. whats going on here is we're checking if you're working
            // then we're checking if the way the time has been entered is valid
            // if it's not valid we're adding one to the count
            // if at the end the count is > 0 we display an error
            // this was my quick way of making sure we don't display an error one million times
            if (hours.mondayStart != " 0")
            {
                if (check.checkValid("Monday") == false)
                {
                    ++count;
                }
            }
            if (hours.tuesdayStart != " 0")
            {
                if (check.checkValid("Tuesday") == false)
                {
                    ++count;
                }
            }
            if (hours.wednesdayStart != " 0")
            {
                if (check.checkValid("Wednesday") == false)
                {
                    ++count;
                }
            }
            if (hours.thursdayStart != " 0")
            {
                if (check.checkValid("Thursday") == false)
                {
                    ++count;
                }
            }
            if (hours.fridayStart != " 0")
            {
                if (check.checkValid("Friday") == false)
                {
                    ++count;
                }
            }
            if (count > 0)
            {
                MessageBox.Show("Please make sure your times are in the correct format (<HH>:<MM>), and/or have '0' in empty spaces", "Error", MessageBoxButtons.OK);
            }
            else if (count == 0)
            {
                MessageBox.Show("Added to calendar!", "Success", MessageBoxButtons.OK);
                calendar.AddToCalendar(true);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            AutoUpdater.Start(@"http://uraqt.xyz/uselessprograms/timesheetupdate.xml");
        }
    }
}
