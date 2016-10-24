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
            string employeeFile = "First Name) " + Environment.NewLine +
                "Last Name) " + Environment.NewLine +
                "NSHE ID) ";
            if (File.Exists("hours.txt") == false)
            {
                File.WriteAllText("hours.txt", hoursFile);
            }
            if (File.Exists("EmployeeInfo.txt") == false)
            {
                File.WriteAllText("EmployeeInfo.txt", employeeFile);
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
            WorkingHours hoursWorked = new WorkingHours();
            DateTime dateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            string fileNameExisting = "time.pdf";
            string fileNameNew = "TimeSheet" + dateValue.Month + dateValue.Year + ".pdf";

            string a = "1st - 15th";
            string b = "16th - 30th";
            string c = "16th - 31st";

            double hoursTotalInt = 0;

            // Employee Info Variables
            string empFirst = File.ReadLines("EmployeeInfo.txt").Skip(0).Take(1).First();
            empFirst = empFirst.Split(')').Last();
            string empLast = File.ReadLines("EmployeeInfo.txt").Skip(1).Take(1).First();
            empLast = empLast.Split(')').Last();
            string empNSHE = File.ReadLines("EmployeeInfo.txt").Skip(2).Take(1).First();
            empNSHE = empNSHE.Split(')').Last();

            // Variables for monday's hours
            string mondayHours = File.ReadLines("hours.txt").Skip(2).Take(1).First();
            mondayHours = mondayHours.Split(')').Last();
            string mondayStart = mondayHours.Split(',').ElementAt(0);
            string mondayBreakStart = mondayHours.Split(',').ElementAt(1);
            string mondayBreakEnd = mondayHours.Split(',').ElementAt(2);
            string mondayEnd = mondayHours.Split(',').ElementAt(3);
            string mondayTotal = mondayHours.Split(',').ElementAt(4);
            
            // Variables for tuesday's hours
            string tuesdayHours = File.ReadLines("hours.txt").Skip(3).Take(1).First();
            tuesdayHours = tuesdayHours.Split(')').Last();
            string tuesdayStart = tuesdayHours.Split(',').ElementAt(0);
            string tuesdayBreakStart = tuesdayHours.Split(',').ElementAt(1);
            string tuesdayBreakEnd = tuesdayHours.Split(',').ElementAt(2);
            string tuesdayEnd = tuesdayHours.Split(',').ElementAt(3);
            string tuesdayTotal = tuesdayHours.Split(',').ElementAt(4);

            // Variables for wednesday's hours
            string wednesdayHours = File.ReadLines("hours.txt").Skip(4).Take(1).First();
            wednesdayHours = wednesdayHours.Split(')').Last();
            string wednesdayStart = wednesdayHours.Split(',').ElementAt(0);
            string wednesdayBreakStart = wednesdayHours.Split(',').ElementAt(1);
            string wednesdayBreakEnd = wednesdayHours.Split(',').ElementAt(2);
            string wednesdayEnd = wednesdayHours.Split(',').ElementAt(3);
            string wednesdayTotal = wednesdayHours.Split(',').ElementAt(4);

            // Variables for Thursday's hours
            string thursdayHours = File.ReadLines("hours.txt").Skip(5).Take(1).First();
            thursdayHours = thursdayHours.Split(')').Last();
            string thursdayStart = thursdayHours.Split(',').ElementAt(0);
            string thursdayBreakStart = thursdayHours.Split(',').ElementAt(1);
            string thursdayBreakEnd = thursdayHours.Split(',').ElementAt(2);
            string thursdayEnd = thursdayHours.Split(',').ElementAt(3);
            string thursdayTotal = thursdayHours.Split(',').ElementAt(4);

            // Variables for Friday's hours
            string fridayHours = File.ReadLines("hours.txt").Skip(6).Take(1).First();
            fridayHours = fridayHours.Split(')').Last();
            string fridayStart = fridayHours.Split(',').ElementAt(0);
            string fridayBreakStart = fridayHours.Split(',').ElementAt(1);
            string fridayBreakEnd = fridayHours.Split(',').ElementAt(2);
            string fridayEnd = fridayHours.Split(',').ElementAt(3);
            string fridayTotal = fridayHours.Split(',').ElementAt(4);

            using (var existingFileStream = new FileStream(fileNameExisting, FileMode.Open))
            using (var newFileStream = new FileStream(fileNameNew, FileMode.Create))
            {
                // Open existing PDF
                var pdfReader = new PdfReader(existingFileStream);

                // PdfStamper, which will create
                var stamper = new PdfStamper(pdfReader, newFileStream);

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
                foreach (string fieldKey in fieldKeys)
                {
                    for (int i = 1; i < x; ++i)
                    {
                        if (a.Equals(listBox1.SelectedItem))
                        {
                            if (fieldKey.Equals("Work StartRow" + i))
                            {
                                switch (dateValue.AddDays(i - 1).ToString("ddd"))
                                {
                                    case "Mon":
                                        form.SetField(fieldKey, mondayStart);
                                        form.SetField("Work EndRow" + i, mondayEnd);
                                        form.SetField("Break OutRow" + i, mondayBreakStart);
                                        form.SetField("Break InRow" + i, mondayBreakEnd);
                                        form.SetField("HoursRow" + i, mondayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(mondayTotal);
                                        break;
                                    case "Tue":
                                        form.SetField(fieldKey, tuesdayStart);
                                        form.SetField("Work EndRow" + i, tuesdayEnd);
                                        form.SetField("Break OutRow" + i, tuesdayBreakStart);
                                        form.SetField("Break InRow" + i, tuesdayBreakEnd);
                                        form.SetField("HoursRow" + i, tuesdayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(tuesdayTotal);
                                        break;
                                    case "Wed":
                                        form.SetField(fieldKey, wednesdayStart);
                                        form.SetField("Work EndRow" + i, wednesdayEnd);
                                        form.SetField("Break OutRow" + i, wednesdayBreakStart);
                                        form.SetField("Break InRow" + i, wednesdayBreakEnd);
                                        form.SetField("HoursRow" + i, wednesdayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(wednesdayTotal);
                                        break;
                                    case "Thu":
                                        form.SetField(fieldKey, thursdayStart);
                                        form.SetField("Work EndRow" + i, thursdayEnd);
                                        form.SetField("Break OutRow" + i, thursdayBreakStart);
                                        form.SetField("Break InRow" + i, thursdayBreakEnd);
                                        form.SetField("HoursRow" + i, thursdayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(thursdayTotal);
                                        break;
                                    case "Fri":
                                        form.SetField(fieldKey, fridayStart);
                                        form.SetField("Work EndRow" + i, fridayEnd);
                                        form.SetField("Break OutRow" + i, fridayBreakStart);
                                        form.SetField("Break InRow" + i, fridayBreakEnd);
                                        form.SetField("HoursRow" + i, fridayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(fridayTotal);
                                        break;
                                    default:

                                        break;
                                }
                            }
                        }

                        else if (b.Equals(listBox1.SelectedItem))
                        {
                            if (fieldKey.Equals("Work StartRow" + i))
                            {
                                switch (dateValue.AddDays(i + 15 - 1).ToString("ddd"))
                                {
                                    case "Mon":
                                        form.SetField(fieldKey, mondayStart);
                                        form.SetField("Work EndRow" + i, mondayEnd);
                                        form.SetField("Break OutRow" + i, mondayBreakStart);
                                        form.SetField("Break InRow" + i, mondayBreakEnd);
                                        form.SetField("HoursRow" + i, mondayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(mondayTotal);
                                        break;
                                    case "Tue":
                                        form.SetField(fieldKey, tuesdayStart);
                                        form.SetField("Work EndRow" + i, tuesdayEnd);
                                        form.SetField("Break OutRow" + i, tuesdayBreakStart);
                                        form.SetField("Break InRow" + i, tuesdayBreakEnd);
                                        form.SetField("HoursRow" + i, tuesdayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(tuesdayTotal);
                                        break;
                                    case "Wed":
                                        form.SetField(fieldKey, wednesdayStart);
                                        form.SetField("Work EndRow" + i, wednesdayEnd);
                                        form.SetField("Break OutRow" + i, wednesdayBreakStart);
                                        form.SetField("Break InRow" + i, wednesdayBreakEnd);
                                        form.SetField("HoursRow" + i, wednesdayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(wednesdayTotal);
                                        break;
                                    case "Thu":
                                        form.SetField(fieldKey, thursdayStart);
                                        form.SetField("Work EndRow" + i, thursdayEnd);
                                        form.SetField("Break OutRow" + i, thursdayBreakStart);
                                        form.SetField("Break InRow" + i, thursdayBreakEnd);
                                        form.SetField("HoursRow" + i, thursdayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(thursdayTotal);
                                        break;
                                    case "Fri":
                                        form.SetField(fieldKey, fridayStart);
                                        form.SetField("Work EndRow" + i, fridayEnd);
                                        form.SetField("Break OutRow" + i, fridayBreakStart);
                                        form.SetField("Break InRow" + i, fridayBreakEnd);
                                        form.SetField("HoursRow" + i, fridayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(fridayTotal);
                                        break;
                                    default:
                                        
                                        break;
                                }
                            }
                        }

                        else if (c.Equals(listBox1.SelectedItem))
                        {
                            x = 17;
                            if (fieldKey.Equals("Work StartRow" + i))
                            {
                                switch (dateValue.AddDays(i + 15 - 1).ToString("ddd"))
                                {
                                    case "Mon":
                                        form.SetField(fieldKey, mondayStart);
                                        form.SetField("Work EndRow" + i, mondayEnd);
                                        form.SetField("Break OutRow" + i, mondayBreakStart);
                                        form.SetField("Break InRow" + i, mondayBreakEnd);
                                        form.SetField("HoursRow" + i, mondayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(mondayTotal);
                                        break;
                                    case "Tue":
                                        form.SetField(fieldKey, tuesdayStart);
                                        form.SetField("Work EndRow" + i, tuesdayEnd);
                                        form.SetField("Break OutRow" + i, tuesdayBreakStart);
                                        form.SetField("Break InRow" + i, tuesdayBreakEnd);
                                        form.SetField("HoursRow" + i, tuesdayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(tuesdayTotal);
                                        break;
                                    case "Wed":
                                        form.SetField(fieldKey, wednesdayStart);
                                        form.SetField("Work EndRow" + i, wednesdayEnd);
                                        form.SetField("Break OutRow" + i, wednesdayBreakStart);
                                        form.SetField("Break InRow" + i, wednesdayBreakEnd);
                                        form.SetField("HoursRow" + i, wednesdayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(wednesdayTotal);
                                        break;
                                    case "Thu":
                                        form.SetField(fieldKey, thursdayStart);
                                        form.SetField("Work EndRow" + i, thursdayEnd);
                                        form.SetField("Break OutRow" + i, thursdayBreakStart);
                                        form.SetField("Break InRow" + i, thursdayBreakEnd);
                                        form.SetField("HoursRow" + i, thursdayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(thursdayTotal);
                                        break;
                                    case "Fri":
                                        form.SetField(fieldKey, fridayStart);
                                        form.SetField("Work EndRow" + i, fridayEnd);
                                        form.SetField("Break OutRow" + i, fridayBreakStart);
                                        form.SetField("Break InRow" + i, fridayBreakEnd);
                                        form.SetField("HoursRow" + i, fridayTotal);
                                        hoursTotalInt = hoursTotalInt + Double.Parse(fridayTotal);
                                        break;
                                    default:

                                        break;
                                }
                            }
                        }

                    }
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

        public void saveDataTable()
        {
            //Resize DataGridView to full height.
            int height = dataGridView1.Height;
            int width = dataGridView1.Width;
            dataGridView1.Height = dataGridView1.Rows.GetRowsHeight(DataGridViewElementStates.None) + dataGridView1.RowHeadersWidth;
            dataGridView1.Width = dataGridView1.Columns.GetColumnsWidth(DataGridViewElementStates.None) + dataGridView1.RowHeadersWidth;

            //Create a Bitmap and draw the DataGridView on it.
            Bitmap bitmap = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));

            //Resize DataGridView back to original height.
            dataGridView1.Height = height;
            dataGridView1.Width = width;

            //Save the Bitmap to folder.
            bitmap.Save(@"DataGridView.png");
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

            WorkingHours hoursWorked = new WorkingHours();
            DateTime dateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            string fileHeader = string.Format("{0}{1}", "Date, Work Start, Break Out, Break In, Work End, Hours", Environment.NewLine);
            string fileFooter = string.Format("{0}{1}", ", , , , Total Hours, 0", Environment.NewLine);

            if (a.Equals(listBox1.SelectedItem))
            {
                ReplacePdfForm();
            }
            
            /*if (a.Equals(listBox1.SelectedItem))
            {
                int i = 1;
                string path = @"Yourmom.txt";
                string text;

                // makes sure the file is empty before writing
                File.WriteAllText(path, "");

                string abvrDay = dateValue.ToString("ddd");

                // Adding the appropriate headers to the file so when the grid is made it doesnt all break
                File.AppendAllText(path, fileHeader);

                for (i = 1; i < 16; ++i)
                {
                    switch (dateValue.AddDays(i - 1).ToString("ddd"))
                    {
                        case "Mon":
                            text = string.Format("{0}/{1}/{2},{3}{4}",DateTime.Now.Month, i, DateTime.Now.Year, mondayHours, Environment.NewLine);
                            File.AppendAllText(path, text);
                            break;
                        case "Tue":
                            text = string.Format("{0}/{1}/{2},{3}{4}", DateTime.Now.Month, i, DateTime.Now.Year, tuesdayHours, Environment.NewLine);
                            File.AppendAllText(path, text);
                            break;
                        case "Wed":
                            text = string.Format("{0}/{1}/{2},{3}{4}", DateTime.Now.Month, i, DateTime.Now.Year, wednesdayHours, Environment.NewLine);
                            File.AppendAllText(path, text);
                            break;
                        case "Thu":
                            text = string.Format("{0}/{1}/{2},{3}{4}", DateTime.Now.Month, i, DateTime.Now.Year, thursdayHours, Environment.NewLine);
                            File.AppendAllText(path, text);
                            break;
                        case "Fri":
                            text = string.Format("{0}/{1}/{2},{3}{4}", DateTime.Now.Month, i, DateTime.Now.Year, fridayHours, Environment.NewLine);
                            File.AppendAllText(path, text);
                            break;
                        default:
                            text = string.Format("{0}/{1}/{2},{3}{4}", DateTime.Now.Month, i, DateTime.Now.Year, "0, 0, 0, 0, 0", Environment.NewLine);
                            File.AppendAllText(path, text);
                            dateValue.AddDays(1);
                            break;
                    }
                }
                
                dataGridView1.DataSource = Helper.DataTableFromTextFile("Yourmom.txt");
                saveDataTable();

                Image backImg = Image.FromFile("timesheet.png");
                Image mrkImg = Image.FromFile("DataGridView.png");
                Graphics g = Graphics.FromImage(backImg);
                g.DrawImage(mrkImg, 30, 390);
                backImg.Save("result.png");

            }*/
            else if (b.Equals(listBox1.SelectedItem))
            {
                ReplacePdfForm();
            }
            else if (c.Equals(listBox1.SelectedItem))
            {
                ReplacePdfForm();
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
            Process.Start("hours.txt");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("EmployeeInfo.txt");
        }
    }
}
