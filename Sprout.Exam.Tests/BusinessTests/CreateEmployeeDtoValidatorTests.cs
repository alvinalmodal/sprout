using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Validators;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Tests.BusinessTests
{
    public class CreateEmployeeDtoValidatorTests
    {
        private CreateEmployeeDtoValidator Validator;
        public CreateEmployeeDtoValidatorTests()
        {
            Validator = new CreateEmployeeDtoValidator();
        }

        private CreateEmployeeDto GenerateInvalidTestData()
        {
            return new CreateEmployeeDto
            {
           
            };
        }

        private CreateEmployeeDto GenerateValidTestData()
        {
            return new CreateEmployeeDto
            {
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
                            new CreateEmployeeDto
                            {
                                Birthdate = DateTime.Now,
                                Tin = "9999",
                                TypeId = (int)EmployeeType.Contractual
                            },
                            @"'Full Name' must not be empty."
                        },
                        new object[] {
                            new CreateEmployeeDto
                            {
                                FullName = "Juan Dela Cruz",
                                Tin = "9999",
                                TypeId = (int)EmployeeType.Contractual
                            },
                            "Invalid birthdate."
                        },
                        new object[] {
                            new CreateEmployeeDto
                            {
                                FullName = "Juan Dela Cruz",
                                Birthdate = DateTime.Now,
                                TypeId = (int)EmployeeType.Contractual
                            },
                            "TIN must be in numeric form with no special characters."
                        },
                        new object[] {
                            new CreateEmployeeDto
                            {
                                FullName = "Juan Dela Cruz",
                                Birthdate = DateTime.Now,
                                Tin = "9999",
                            },
                            "Invalid type id was provided."
                        }
        };

        [Fact]
        public void Validate_CreateEmployeeDtoShouldReturnIsValidFalse()
        {
            var employee = GenerateInvalidTestData();
            var results = Validator.Validate(employee);
            Assert.False(results.IsValid);
        }

        [Fact]
        public void Validate_CreateEmployeeShouldReturnTrue()
        {
            var employee = GenerateValidTestData();
            var results = Validator.Validate(employee);
            Assert.True(results.IsValid);
        }

        [Theory]
        [MemberData(nameof(GetInvalidTestDataByFields))]
        public void Validate_CreateEmployeeShouldReturnErrorPerField(CreateEmployeeDto employee, string expectedErrorMessage)
        {
            var results = Validator.Validate(employee);
            Assert.Equal(expectedErrorMessage,results.Errors.First().ErrorMessage);
        }
    }
}
