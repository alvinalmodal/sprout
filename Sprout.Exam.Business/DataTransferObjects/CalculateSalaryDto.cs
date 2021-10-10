using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class CalculateSalaryDto
    {
        public int Id { get; set; }
        public double AbsentDays { get; set; }
        public double WorkedDays { get; set; }
    }
}
