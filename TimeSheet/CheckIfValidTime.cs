using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeSheet
{
    class CheckIfValidTime
    {
        public bool checkValid(string dayOfWeek)
        {
            ParseHours hours = new ParseHours();
            hours.HoursWorked();
            bool valid = true;

            // This whole thing is fucky
            // We're taking the day of the week, checking if it's a working day (has no 0 in the first field for the day)
            // If it's a working day, both mondayStart and end should include ':'
            // if they don't it's not a valid time
            // This is for both a bit of standardization as well as ensuring that the time works with the calendar update
            if (dayOfWeek == "Monday")
            {
                if (hours.mondayStart != "0" || hours.mondayStart != " 0")
                {
                    if (hours.mondayStart.Contains(":") == false || hours.mondayEnd.Contains(":") == false)
                    {
                        valid = false;
                    }
                }
                else if (hours.mondayStart == "0" || hours.mondayStart == " 0")
                {
                    valid = true;
                }
            }
            if (dayOfWeek == "Tuesday")
            {
                if (hours.tuesdayStart != "0" || hours.tuesdayStart != " 0")
                {
                    if (hours.tuesdayStart.Contains(":") == false || hours.tuesdayEnd.Contains(":") == false)
                    {
                        valid = false;
                    }
                }
                else if (hours.tuesdayStart == "0" || hours.tuesdayStart == " 0")
                {
                    valid = true;
                }
            }
            if (dayOfWeek == "Wednesday")
            {
                if (hours.wednesdayStart != "0" || hours.wednesdayStart != " 0")
                {
                    if (hours.wednesdayStart.Contains(":") == false || hours.wednesdayEnd.Contains(":") == false)
                    {
                        valid = false;
                    }
                }
                else if (hours.wednesdayStart == "0" || hours.wednesdayStart == " 0")
                {
                    valid = true;
                }
            }
            if (dayOfWeek == "Thursday")
            {
                if (hours.thursdayStart != "0" || hours.thursdayStart != " 0")
                {
                    if (hours.thursdayStart.Contains(":") == false || hours.thursdayEnd.Contains(":") == false)
                    {
                        valid = false;
                    }
                }
                else if (hours.thursdayStart == "0" || hours.thursdayStart == " 0")
                {
                    valid = true;
                }
            }
            if (dayOfWeek == "Friday")
            {
                if (hours.fridayStart != "0" || hours.fridayStart != " 0")
                {
                    if (hours.fridayStart.Contains(":") == false || hours.fridayEnd.Contains(":") == false)
                    {
                        valid = false;
                    }
                }
                else if (hours.fridayStart == "0" || hours.fridayStart == " 0")
                {
                    valid = true;
                }
            }

            return valid;
        }
    }
}
