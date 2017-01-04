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
        public bool checkValid()
        {
            ParseHours hours = new ParseHours();
            hours.HoursWorked();
            bool valid = true;

            if (hours.mondayStart.Contains(":") == false || hours.mondayEnd.Contains(":") == false
                || hours.tuesdayStart.Contains(":") == false || hours.tuesdayEnd.Contains(":") == false
                || hours.wednesdayStart.Contains(":") == false || hours.wednesdayEnd.Contains(":") == false
                || hours.thursdayStart.Contains(":") == false || hours.thursdayEnd.Contains(":") == false
                || hours.fridayStart.Contains(":") == false || hours.fridayEnd.Contains(":") == false)
            {
                valid = false;
            }

            return valid;
        }
    }
}
