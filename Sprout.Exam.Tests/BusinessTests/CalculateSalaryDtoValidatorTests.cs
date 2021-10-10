using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Validators;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Implementations;
using Sprout.Exam.DataAccess.Models;
using Sprout.Exam.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Tests.BusinessTests
{
    public class CalculateSalaryDtoValidatorTests
    {
        private EmployeeRepository Db;
        private TestDBHelper DbHelper;
        private static EmployeeModel ContractualEmployee;
        private static EmployeeModel RegularEmployee;
        private CalculateSalaryDtoValidator Validator;

        public CalculateSalaryDtoValidatorTests()
        {
            var configBuilder = new ConfigBuilder();
            var config = configBuilder.Build();
            Db = new EmployeeRepository(config);
            DbHelper = new TestDBHelper(config);
            Validator = new CalculateSalaryDtoValidator(config);
            CreateTestEmployees();
        }

        private void CreateTestEmployees()
        {
            DbHelper.TruncateTable("Employee");
            ContractualEmployee = new EmployeeModel();
            RegularEmployee = new EmployeeModel();
            ContractualEmployee = Db.Save(new EmployeeModel
            {
                FullName = "Juan Dela Cruz",
                BirthDate = DateTime.Now,
                TIN = "9999",
                EmployeeTypeId = EmployeeType.Contractual
            }).Result;
            RegularEmployee = Db.Save(new EmployeeModel
            {
                FullName = "Pedro Dela Cruz",
                BirthDate = DateTime.Now,
                TIN = "8888",
                EmployeeTypeId = EmployeeType.Regular
            }).Result;
        }


        public static IEnumerable<object[]> GetInvalidTestData =>
            new List<object[]>
            {
                        new object[] {
                            new CalculateSalaryDto
                            {
                                AbsentDays = 0,
                                WorkedDays = 0
                            },
                            "Id provided is invalid.",
                        },
                        new object[] {
                            new CalculateSalaryDto
                            {
                                Id = 2, // id for regular employee
                            },
                            "Absent days is required.",
                        },
                        new object[] {
                            new CalculateSalaryDto
                            {
                                Id = 1 // id for contractual employee
                            },
                            "Worked days is required.",
                        },
            };

        public static IEnumerable<object[]> GetValidTestData =>
        new List<object[]>
        {
                            new object[] {
                                new CalculateSalaryDto
                                {
                                    Id = 2, // id for regular employee
                                    AbsentDays = 15
                                },
                                0,
                            },
                            new object[] {
                                new CalculateSalaryDto
                                {
                                    Id = 1,
                                    WorkedDays = 10 // id for contractual employee
                                },
                                0,
                            },
        };

        [Theory]
        [MemberData(nameof(GetValidTestData))]
        public void Validate_ShouldReturnNoErrorsIfValid(CalculateSalaryDto employee, int expectedErrorCount)
        {
            var result = Validator.Validate(employee);

            Assert.Equal(expectedErrorCount, result.Errors?.Count);
        }

        [Theory]
        [MemberData(nameof(GetInvalidTestData))]
        public void Validate_ShouldReturnCorrectErrorMessagePerField(CalculateSalaryDto employee, string expectedErrorMessage)
        {
            var result = Validator.Validate(employee);

            Assert.Equal(expectedErrorMessage, result.Errors.First().ErrorMessage);
        }
    }
}
