using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ValidationRules
{
    public class ImageValidator : AbstractValidator<Image>
    {
        public ImageValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Görsel başlığı boş olamaz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Görsel açıklaması boş olamaz");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel yolu boş olamaz");
            RuleFor(x => x.Title).MaximumLength(20).WithMessage("En fazla 20 karakter");
            RuleFor(x => x.Title).MinimumLength(8).WithMessage("En fazla 20 karakter");
            RuleFor(x => x.Description).MaximumLength(50).WithMessage("En fazla 50 karakter");
            RuleFor(x => x.Description).MinimumLength(20).WithMessage("En az 20 karakter");
        }
    }
}
