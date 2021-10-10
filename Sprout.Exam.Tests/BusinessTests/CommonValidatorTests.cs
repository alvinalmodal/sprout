using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Tests.BusinessTests
{
    public class CommonValidatorTests
    {
        private CommonValidators Validator;
        public CommonValidatorTests()
        {
            Validator = new CommonValidators();
        }

        public static IEnumerable<object[]> GetTestData =>
            new List<object[]>
            {
                        new object[] {
                            new EditEmployeeDto
                            {
                                FullName = "Juan Dela Cruz",
                                Tin = "9999",
                                TypeId = (int)EmployeeType.Contractual
                            },
                            true,
                        },
                        new object[] {
                            new EditEmployeeDto
                            {
                                FullName = "Juan Dela Cruz",
                                Tin = "9999",
                                TypeId = (int)EmployeeType.Contractual,
                                Birthdate = DateTime.Now
                            },
                            false,
                        },
            };

        [Theory]
        [InlineData("99999", true)]
        [InlineData("999-9", false)]
        [InlineData("123",true)]
        [InlineData("test", false)]
        public void IsValidTin_ShouldCheckIfTinIsValid(string tin, bool expected)
        {
            var actual = Validator.IsValidTin(tin);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(2,true)]
        [InlineData(3,false)]
        public void IsValidEmployeeType_ShouldReturnTrueIfValidFalseIfInvalid(int typeId, bool expected)
        {
            var actual = Validator.IsValidEmployeeType(typeId);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void IsDefaultDateTime_ShouldReturnTrueIfValid_FalseIfInvalid(EditEmployeeDto employee, bool expected)
        {
            var actual = Validator.IsDefaultDateTime(employee.Birthdate);
            Assert.Equal(expected, actual);
        }
    }
}
