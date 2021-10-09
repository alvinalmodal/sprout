using Sprout.Exam.Business.Factories;
using Sprout.Exam.Business.Implementations;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Business.Models;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using Xunit;

namespace Sprout.Exam.Tests.BusinessTests
{
    public class SalaryCalculatorFactoryTests
    {
        public static IEnumerable<object[]> GetClassNameTestData =>
                new List<object[]>
                {
                    new object[] {
                        new ComputationDetailsModel {
                            RatePerDay = 500,
                            DaysReported = 15.5,
                            TypeId = EmployeeType.Contractual
                        },
                        typeof(ContractualEmployeeSalaryCalculator).Name
                    },
                    new object[] {
                        new ComputationDetailsModel {
                            MonthlySalary = 20000,
                            Absences = 1,
                            TaxRate = 0.12,
                            TypeId = EmployeeType.Regular
                        },
                        typeof(RegularEmployeeSalaryCalculator).Name
                    },
                };

        public static IEnumerable<object[]> GetComputationTestData =>
        new List<object[]>
        {
                    new object[] {
                        new ComputationDetailsModel {
                            RatePerDay = 500,
                            DaysReported = 15.5,
                            TypeId = EmployeeType.Contractual
                        },
                          7_750.00
                    },
                    new object[] {
                        new ComputationDetailsModel {
                            MonthlySalary = 20000,
                            Absences = 1,
                            TaxRate = 0.12,
                            TypeId = EmployeeType.Regular
                        },
                        16_690.91
                    },
        };

        [Theory]
        [MemberData(nameof(GetClassNameTestData))]
        public void GetCalculator_ShouldReturnCorrectCalculatorClass(ComputationDetailsModel payrollDetails, string className)
        {
            var calculatorFactory = new SalaryCalculatorFactory();
            var calculator = calculatorFactory.GetCalculator(payrollDetails);
            Assert.Equal(calculator.GetType().Name, className);
        }

        [Theory]
        [MemberData(nameof(GetComputationTestData))]
        public void GetCalculator_ShouldReturnCorrectComputation(ComputationDetailsModel payrollDetails, double netIncome)
        {
            var calculatorFactory = new SalaryCalculatorFactory();
            var calculator = calculatorFactory.GetCalculator(payrollDetails);
            Assert.Equal(netIncome, calculator.ComputeSalary());
        }
    }
}
