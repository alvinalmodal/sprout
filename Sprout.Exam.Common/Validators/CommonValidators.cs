using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Common.Validators
{
    public class CommonValidators
    {
        public bool IsValidTin(string tin)
        {
            int convertedValue = 0;
            int.TryParse(tin, out convertedValue);

            if (convertedValue == 0)
                return false;

            return true;
        }

        public bool IsValidEmployeeType(int typeId)
        {
            if (Enum.IsDefined(typeof(EmployeeType), typeId))
                return true;
            return false;
        }

        public bool IsDefaultDateTime(DateTime datetime)
        {
            if (datetime.ToString("yyyy/MM/dd") == "0001/01/01")
                return true;
            return false;
        }
    }
}
