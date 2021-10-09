using Sprout.Exam.Business.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Tests.BusinessTests
{
    public class ContractualEmployeeSalaryCalculatorTests
    {
        [Fact]
        public void Compute_ShouldReturnCorrectContractualPayout()
        {
            var expected = 7_750.00;

            var calculator = new ContractualEmployeeSalaryCalculator(ratePerDay: 500, daysReported: 15.5);
            var actual = calculator.ComputeSalary();

            Assert.Equal(expected, actual);
        }
    }
}
