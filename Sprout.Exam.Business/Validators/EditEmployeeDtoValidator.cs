using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Validators;
using Sprout.Exam.DataAccess.Implementations;

namespace Sprout.Exam.Business.Validators
{
    public class EditEmployeeDtoValidator : AbstractValidator<EditEmployeeDto>
    {
        public EditEmployeeDtoValidator(IConfiguration config)
        {
            var commonValidator = new CommonValidators();
            var employeeDb = new EmployeeRepository(config);

            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).Must(value => employeeDb.SearchById(value).Result != null)
                .WithMessage("Id provided is invalid.");
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Tin).Must(value => commonValidator.IsValidTin(value) == true)
                .WithMessage("TIN must be in numeric form with no special characters.");
            RuleFor(x => x.Birthdate).NotNull().Must(value => commonValidator.IsDefaultDateTime(value) == false)
                .WithMessage("Invalid birthdate.");
            RuleFor(x => x.TypeId).Must(value => commonValidator.IsValidEmployeeType(value) == true)
                .WithMessage("Invalid type id was provided.");
        }
    }
}
