using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Validators;

namespace Sprout.Exam.Business.Validators
{
    public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidator()
        {
            var commonValidator = new CommonValidators();

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
