using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u=>u.FirsName).NotEmpty();
            RuleFor(u=>u.LastName).NotEmpty();
            RuleFor(u=>u.Email).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();

            RuleFor(u=>u.Password).NotEmpty();
            RuleFor(u => u.Password).MinimumLength(3);
            RuleFor(u => u.Password).MaximumLength(20);
        }
    }
}
