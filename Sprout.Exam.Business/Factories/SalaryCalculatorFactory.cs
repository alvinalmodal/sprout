using System;
using System.Collections.Generic;
using System.Text;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Business.Implementations;
using Sprout.Exam.Business.Models;
using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.Business.Factories
{
    public class SalaryCalculatorFactory
    {
        public ISalaryCalculator GetCalculator(ComputationDetailsModel payrollDetails)
        {
            ISalaryCalculator calculator = null;

            switch(payrollDetails.TypeId)
            {
                case EmployeeType.Regular:
                    calculator = new RegularEmployeeSalaryCalculator(
                        payrollDetails.MonthlySalary,
                        payrollDetails.Absences,
                        payrollDetails.TaxRate
                    );
                    break;
                case EmployeeType.Contractual:
                    calculator = new ContractualEmployeeSalaryCalculator(
                        payrollDetails.RatePerDay, 
                        payrollDetails.DaysReported
                     );
                    break;
                default:
                    break;
            }

            return calculator;
        }
    }
}
