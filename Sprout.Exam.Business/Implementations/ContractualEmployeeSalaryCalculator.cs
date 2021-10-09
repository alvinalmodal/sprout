using Sprout.Exam.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Implementations
{
    public class ContractualEmployeeSalaryCalculator : ISalaryCalculator
    {
        private readonly double RatePerDay;
        private readonly double DaysReported;

        public ContractualEmployeeSalaryCalculator(double ratePerDay, double daysReported)
        {
            RatePerDay = ratePerDay;
            DaysReported = daysReported;
        }

        public double ComputeSalary()
        {
            var netIncome = Math.Round(RatePerDay * DaysReported, 2);
            return netIncome;
        }
    }
}
