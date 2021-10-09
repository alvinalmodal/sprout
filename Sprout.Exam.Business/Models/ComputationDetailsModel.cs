using System;
using System.Collections.Generic;
using System.Text;
using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.Business.Models
{
    public class ComputationDetailsModel
    {
        public EmployeeType TypeId { get; set; }
        public double RatePerDay { get; set; }
        public double DaysReported { get; set; }
        public double Absences { get; set; }
        public double MonthlySalary { get; set; }
        public double TaxRate { get; set; }
    }
}
