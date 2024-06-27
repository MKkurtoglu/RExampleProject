using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public  class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c=>c.BrandId).NotEmpty();
            RuleFor(c=>c.ColorId).NotEmpty();


            RuleFor(c=>c.Description).NotEmpty();
            RuleFor(c => c.Description).MinimumLength(10);
            RuleFor(c => c.Description).MaximumLength(100);


            RuleFor(c=>c.ModelYear).NotEmpty();
            RuleFor(c => c.ModelYear).Must(Control).WithMessage("Araba modeli 5 yıldan eski olamaz");


            RuleFor(c => c.DailyPrice).GreaterThan(650);
        }

        private bool Control(int arg)
        {
            var result = DateTime.Now.Year - arg;
            if (result > 5)
            {
                return false;
            }
            return true;
        }
    }
}
