using Microsoft.Extensions.Configuration;
using Sprout.Exam.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Implementations
{
    public class RegularEmployeeSalaryCalculator : ISalaryCalculator
    {
        private readonly double MonthlySalary;
        private readonly double Absences;
        private readonly double TaxRate;

        public RegularEmployeeSalaryCalculator(double monthlySalary, double absences, double taxRate)
        {
            MonthlySalary = monthlySalary;
            Absences = absences;
            TaxRate = taxRate;
        }

        public double ComputeSalary()
        {
            var netIncome = 0.00;

            double dailyRate = (MonthlySalary / 22);
            double taxDeduction = TaxRate * MonthlySalary;
            double absencesDeduction = dailyRate * Absences;

            netIncome = Math.Round(MonthlySalary - (taxDeduction + absencesDeduction),2);

            return netIncome;
        }
    }
}
