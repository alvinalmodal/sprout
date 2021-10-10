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
    public class EditEmployeeDtoValidatorTests
    {
        private EditEmployeeDtoValidator Validator;
        private EmployeeRepository Db;
        private TestDBHelper DbHelper;

        public EditEmployeeDtoValidatorTests()
        {
            var configBuilder = new ConfigBuilder();
            var config = configBuilder.Build();
            Validator = new EditEmployeeDtoValidator(config);
            Db = new EmployeeRepository(config);
            DbHelper = new TestDBHelper(config);
        }

        private EditEmployeeDto GenerateInvalidTestData()
        {
            return new EditEmployeeDto
            {

            };
        }

        private EditEmployeeDto GenerateValidTestData()
        {
            return new EditEmployeeDto
            {
                Id = 1,
                FullName = "Juan Dela Cruz",
                Birthdate = DateTime.Now,
                Tin = "9999",
                TypeId = (int)EmployeeType.Contractual
            };
        }

        public static IEnumerable<object[]> GetInvalidTestDataByFields =>
            new List<object[]>
            {
                        new object[] {
                            new EditEmployeeDto
                            {
                                FullName = "Juan Dela Cruz",
                                Birthdate = DateTime.Now,
                                Tin = "9999",
                                TypeId = (int)EmployeeType.Contractual
                            },
                            @"'Id' must not be empty."
                        },
                        new object[] {
                            new EditEmployeeDto
                            {
                                Id = 999999,
                                FullName = "Juan Dela Cruz",
                                Birthdate = DateTime.Now,
                                Tin = "9999",
                                TypeId = (int)EmployeeType.Contractual
                            },
                            "Id provided is invalid."
                        },
                        new object[] {
                            new EditEmployeeDto
                            {
                                Id = 1,
                                Birthdate = DateTime.Now,
                                Tin = "9999",
                                TypeId = (int)EmployeeType.Contractual
                            },
                            @"'Full Name' must not be empty."
                        },
                        new object[] {
                            new EditEmployeeDto
                            {
                                Id = 1,
                                FullName = "Juan Dela Cruz",
                                Tin = "9999",
                                TypeId = (int)EmployeeType.Contractual
                            },
                            "Invalid birthdate."
                        },
                        new object[] {
                            new EditEmployeeDto
                            {
                                Id = 1,
                                FullName = "Juan Dela Cruz",
                                Birthdate = DateTime.Now,
                                TypeId = (int)EmployeeType.Contractual
                            },
                            "TIN must be in numeric form with no special characters."
                        },
                        new object[] {
                            new EditEmployeeDto
                            {
                                Id = 1,
                                FullName = "Juan Dela Cruz",
                                Birthdate = DateTime.Now,
                                Tin = "9999",
                                TypeId = 999
                            },
                            "Invalid type id was provided."
                        }
            };

        [Fact]
        public void Validate_EditEmployeeDtoShouldReturnIsValidFalse()
        {
            var employee = GenerateInvalidTestData();

            var results = Validator.Validate(employee);

            Assert.False(results.IsValid);
        }

        private void CreateTestEmployee(EditEmployeeDto employee)
        {
            DbHelper.TruncateTable("Employee");
            var savedEmployee = Db.Save(new EmployeeModel
            {
                FullName = employee.FullName,
                BirthDate = employee.Birthdate,
                TIN = employee.Tin,
                EmployeeTypeId = (EmployeeType)employee.TypeId
            }).Result;
        }

        [Fact]
        public void Validate_EditEmployeeShouldReturnTrue()
        {
            var employee = GenerateValidTestData();
            var results = Validator.Validate(employee);

            Assert.True(results.IsValid);
        }

        [Theory]
        [MemberData(nameof(GetInvalidTestDataByFields))]
        public void Validate_CreateEmployeeShouldReturnErrorPerField(EditEmployeeDto employee, string expectedErrorMessage)
        {
            CreateTestEmployee(GenerateValidTestData());
            var results = Validator.Validate(employee);
            Assert.Equal(expectedErrorMessage, results.Errors.First().ErrorMessage);
        }
    }
}
