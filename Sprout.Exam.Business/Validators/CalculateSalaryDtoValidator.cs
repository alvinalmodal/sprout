using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Sprout.Exam.Business.DataTransferObjects;

namespace Sprout.Exam.Business.Validators
{
    public class CalculateSalaryDtoValidator : AbstractValidator<CalculateSalaryDto>
    {
        public CalculateSalaryDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.AbsentDays).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.WorkedDays).NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
