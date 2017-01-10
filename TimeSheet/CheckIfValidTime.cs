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
