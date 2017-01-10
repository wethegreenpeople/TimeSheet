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

namespace TimeSheet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                client.DownloadFile("http://uraqt.xyz/uselessprograms/time.pdf", "time.pdf");
            }
            if (File.Exists("client_secret.json") == false)
            {
                WebClient client = new WebClient();
                client.DownloadFile("http://uraqt.xyz/uselessprograms/client_secret.json", "client_secret.json");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        public class Helper
        {

            public static DataTable DataTableFromTextFile(string location, char delimiter = ',')
            {
                DataTable result;

                string[] LineArray = File.ReadAllLines(location);

                result = FormDataTable(LineArray, delimiter);

                return result;
            }


            private static DataTable FormDataTable(string[] LineArray, char delimiter)
            {
                bool IsHeaderSet = false;

                DataTable dt = new DataTable();

                AddColumnToTable(LineArray, delimiter, ref dt);

                AddRowToTable(LineArray, delimiter, ref dt);

                return dt;
            }


            private static void AddRowToTable(string[] valueCollection, char delimiter, ref DataTable dt)
            {

                for (int i = 1; i < valueCollection.Length; i++)
                {
                    string[] values = valueCollection[i].Split(delimiter);

                    DataRow dr = dt.NewRow();

                    for (int j = 0; j < values.Length; j++)
                    {
                        dr[j] = values[j];
                    }

                    dt.Rows.Add(dr);
                }
            }


            private static void AddColumnToTable(string[] columnCollection, char delimiter, ref DataTable dt)
            {
                string[] columns = columnCollection[0].Split(delimiter);

                foreach (string columnName in columns)
                {
                    DataColumn dc = new DataColumn(columnName, typeof(string));
                    dt.Columns.Add(dc);
                }

            }

        }

        public void ReplacePdfForm()
        {
            DateTime dateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            string fileNameExisting = "time.pdf";
            string fileNameNew = "TimeSheet" + dateValue.Month + dateValue.Year + ".pdf";

            string a = "1st - 15th";
            string b = "16th - 30th";
            string c = "16th - 31st";

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

                // Adding the dates to the table
                foreach (string fieldKey in fieldKeys)
                {
                    for (int i = 1; i < 16; ++i)
                    {
                        if (a.Equals(listBox1.SelectedItem))
                        {
                            if (fieldKey.Equals("DateRow" + i))
                            {
                                string dateText = string.Format("{0}/{1}/{2}", DateTime.Now.Month, i, DateTime.Now.Year);
                                form.SetField(fieldKey, dateText);
                            }
                        }
                        if (b.Equals(listBox1.SelectedItem))
                        {
                            if (fieldKey.Equals("DateRow" + i))
                            {
                                string dateText = string.Format("{0}/{1}/{2}", DateTime.Now.Month, i + 15, DateTime.Now.Year);
                                form.SetField(fieldKey, dateText);
                            }
                        }
                        if (c.Equals(listBox1.SelectedItem))
                        {
                            if (fieldKey.Equals("DateRow" + i))
                            {
                                string dateText = string.Format("{0}/{1}/{2}", DateTime.Now.Month, i + 15, DateTime.Now.Year);
                                form.SetField(fieldKey, dateText);
                            }
                            string extraDateText = string.Format("{0}/31/{2}", DateTime.Now.Month, i + 15, DateTime.Now.Year);
                            form.SetField("DateRow16", extraDateText);
                        }
                        
                    }
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
                            if (a.Equals(listBox1.SelectedItem))
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
                                    }
                                }
                            }

                            //16th - 30th
                            else if (b.Equals(listBox1.SelectedItem))
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
                                    }
                                }
                            }

                            //16th - 30th
                            if (c.Equals(listBox1.SelectedItem))
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
                                    }
                                }


                            }

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                


                foreach (string fieldKey in fieldKeys)
                {
                    switch (fieldKey)
                    {
                        case "Last Name":
                            form.SetField(fieldKey, empLast);
                            break;
                        case "First Name":
                            form.SetField(fieldKey, empFirst);
                            break;
                        case "Pay Period From To":
                            if (a.Equals(listBox1.SelectedItem))
                            {
                                string fromDate = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
                                form.SetField(fieldKey, fromDate);
                            }
                            if (b.Equals(listBox1.SelectedItem))
                            {
                                string fromDate = DateTime.Now.Month + "/16/" + DateTime.Now.Year;
                                form.SetField(fieldKey, fromDate);
                            }
                            else if (c.Equals(listBox1.SelectedItem))
                            {
                                string fromDate = DateTime.Now.Month + "/16/" + DateTime.Now.Year;
                                form.SetField(fieldKey, fromDate);
                            }
                            break;
                        case "Pay Period To":
                            if (a.Equals(listBox1.SelectedItem))
                            {
                                string fromDate = DateTime.Now.Month + "/15/" + DateTime.Now.Year;
                                form.SetField(fieldKey, fromDate);
                            }
                            if (b.Equals(listBox1.SelectedItem))
                            {
                                string fromDate = DateTime.Now.Month + "/30/" + DateTime.Now.Year;
                                form.SetField(fieldKey, fromDate);
                            }
                            else if (c.Equals(listBox1.SelectedItem))
                            {
                                string fromDate = DateTime.Now.Month + "/31/" + DateTime.Now.Year;
                                form.SetField(fieldKey, fromDate);
                            }
                            break;
                        case "Employee ID":
                            form.SetField(fieldKey, empNSHE);
                            break;
                        case "HoursTotal Hours":
                            form.SetField(fieldKey, Convert.ToString(hoursTotalInt));
                            break;
                    }

                }

                stamper.Close();
                pdfReader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PDFEdit editpdf = new PDFEdit();

            string a = "1st - 15th";
            string b = "16th - 30th";
            string c = "16th - 31st";

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

            if (a.Equals(listBox1.SelectedItem))
            {
                ReplacePdfForm();
                MessageBox.Show("Timesheet generated!", "Success", MessageBoxButtons.OK);
            }
            
            else if (b.Equals(listBox1.SelectedItem))
            {
                ReplacePdfForm();
                MessageBox.Show("Timesheet generated!", "Success", MessageBoxButtons.OK);
            }
            else if (c.Equals(listBox1.SelectedItem))
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


        private void clearTable()
        {
            throw new NotImplementedException();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditHours hours = new EditHours();

            hours.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditEmpInfo empInfo = new EditEmpInfo();

            empInfo.Show();
        }

        public string dateRange;
        
        // Calendar update
        private void button4_Click(object sender, EventArgs e)
        {
            CalendarUpdate calendar = new CalendarUpdate();

            string a = "1st - 15th";
            string b = "16th - 30th";
            string c = "16th - 31st";

            if (a.Equals(listBox1.SelectedItem))
            {
                calendar.dateRange = a;
            }

            else if (b.Equals(listBox1.SelectedItem))
            {
                calendar.dateRange = b;
            }
            else if (c.Equals(listBox1.SelectedItem))
            {
                calendar.dateRange = c;
            }
            else
            {
                MessageBox.Show("Please select a date range before trying to update your calendar", "Error", MessageBoxButtons.OK);
            }
            
            ParseHours hours = new ParseHours();
            hours.HoursWorked();
            CheckIfValidTime check = new CheckIfValidTime();
            int count = 0;
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
    }
}
