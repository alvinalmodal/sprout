using Sprout.Exam.Business.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Tests.BusinessTests
{
    public class RegularyEmployeeSalaryCalculatorTests
    {
        [Fact]
        public void Compute_ShouldReturnCorrectPayoutComputation()
        {
            var expected = 16_690.91;

            var calculator = new RegularEmployeeSalaryCalculator(monthlySalary: 20_000.00, absences: 1, taxRate: 0.12);
            var actual = calculator.ComputeSalary();

            Assert.Equal(expected, actual);
        }
    }
}
