using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TimeSheet
{
    class ParseEmpInfo
    {
        public string empFirst { get; set; }
        public string empLast { get; set; }
        public string empNSHE { get; set; }

        public void GetEmployeeInfo()
        {
            empFirst = File.ReadLines("EmployeeInfo.txt").Skip(0).Take(1).First();
            empFirst = empFirst.Split(')').Last().Trim();

            empLast = File.ReadLines("EmployeeInfo.txt").Skip(1).Take(1).First();
            empLast = empLast.Split(')').Last().Trim();

            empNSHE = File.ReadLines("EmployeeInfo.txt").Skip(2).Take(1).First();
            empNSHE = empNSHE.Split(')').Last().Trim();
        }

    }
}
