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

namespace TimeSheet
{
    class ParseHours
    {
        DateTime dateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        public string mondayStart, mondayEnd, mondayBreakStart, mondayBreakEnd, mondayTotal;
        public string tuesdayStart, tuesdayEnd, tuesdayBreakStart, tuesdayBreakEnd, tuesdayTotal;
        public string wednesdayStart, wednesdayEnd, wednesdayBreakStart, wednesdayBreakEnd, wednesdayTotal;
        public string thursdayStart, thursdayEnd, thursdayBreakStart, thursdayBreakEnd, thursdayTotal;
        public string fridayStart, fridayEnd, fridayBreakStart, fridayBreakEnd, fridayTotal;

        public void HoursWorked()
        {
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
            mondayStart = mondayHours.Split(',').ElementAt(0);
            mondayBreakStart = mondayHours.Split(',').ElementAt(1);
            mondayBreakEnd = mondayHours.Split(',').ElementAt(2);
            mondayEnd = mondayHours.Split(',').ElementAt(3);
            mondayTotal = mondayHours.Split(',').ElementAt(4).TrimStart(' ');

            // Variables for tuesday's hours
            string tuesdayHours = File.ReadLines("hours.txt").Skip(3).Take(1).First();
            tuesdayHours = tuesdayHours.Split(')').Last();
            tuesdayStart = tuesdayHours.Split(',').ElementAt(0);
            tuesdayBreakStart = tuesdayHours.Split(',').ElementAt(1);
            tuesdayBreakEnd = tuesdayHours.Split(',').ElementAt(2);
            tuesdayEnd = tuesdayHours.Split(',').ElementAt(3);
            tuesdayTotal = tuesdayHours.Split(',').ElementAt(4).TrimStart(' ');

            // Variables for wednesday's hours
            string wednesdayHours = File.ReadLines("hours.txt").Skip(4).Take(1).First();
            wednesdayHours = wednesdayHours.Split(')').Last();
            wednesdayStart = wednesdayHours.Split(',').ElementAt(0);
            wednesdayBreakStart = wednesdayHours.Split(',').ElementAt(1);
            wednesdayBreakEnd = wednesdayHours.Split(',').ElementAt(2);
            wednesdayEnd = wednesdayHours.Split(',').ElementAt(3);
            wednesdayTotal = wednesdayHours.Split(',').ElementAt(4).TrimStart(' ');

            // Variables for Thursday's hours
            string thursdayHours = File.ReadLines("hours.txt").Skip(5).Take(1).First();
            thursdayHours = thursdayHours.Split(')').Last();
            thursdayStart = thursdayHours.Split(',').ElementAt(0);
            thursdayBreakStart = thursdayHours.Split(',').ElementAt(1);
            thursdayBreakEnd = thursdayHours.Split(',').ElementAt(2);
            thursdayEnd = thursdayHours.Split(',').ElementAt(3);
            thursdayTotal = thursdayHours.Split(',').ElementAt(4).TrimStart(' ');

            // Variables for Friday's hours
            string fridayHours = File.ReadLines("hours.txt").Skip(6).Take(1).First();
            fridayHours = fridayHours.Split(')').Last();
            fridayStart = fridayHours.Split(',').ElementAt(0);
            fridayBreakStart = fridayHours.Split(',').ElementAt(1);
            fridayBreakEnd = fridayHours.Split(',').ElementAt(2);
            fridayEnd = fridayHours.Split(',').ElementAt(3);
            fridayTotal = fridayHours.Split(',').ElementAt(4).TrimStart(' ');
        }
    }
}
