using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ValidationRules
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim boş geçilemez");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Görev boş geçilemez");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel yolu boş geçilemez");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("50 karakterden az olmalı");
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("5 karakterden fazla olmalı");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("50 karakterden az olmalı");
            RuleFor(x => x.Title).MinimumLength(5).WithMessage("5 karakterden fazla olmalı");

        }
    }
}
