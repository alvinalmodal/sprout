using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Implementations;

namespace Sprout.Exam.Business.Validators
{
    public class CalculateSalaryDtoValidator : AbstractValidator<CalculateSalaryDto>
    {
        public CalculateSalaryDtoValidator(IConfiguration config)
        {
            var employeeDb = new EmployeeRepository(config);
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).Must(value => employeeDb.SearchById(value).Result != null)
                .WithMessage("Id provided is invalid.");
            RuleFor(x => x.AbsentDays).NotEmpty()
                .When(employee => employeeDb.SearchById(employee.Id).Result?.EmployeeTypeId == EmployeeType.Regular)
                .WithMessage(employee => $"Absent days is required.");
            RuleFor(x => x.WorkedDays).NotEmpty()
                .When(employee => employeeDb.SearchById(employee.Id).Result?.EmployeeTypeId == EmployeeType.Contractual)
                .WithMessage(employee => $"Worked days is required.");
        }
    }
}
