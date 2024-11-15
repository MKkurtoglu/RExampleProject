﻿using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r=>r.CarId).NotEmpty();
            RuleFor(r=>r.CustomerId).NotEmpty();

            RuleFor(r=>r.RentDate).NotEmpty();
            RuleFor(r=>r.ReturnDate).NotEmpty();
            RuleFor(r=>r.ReturnDate).GreaterThan(r=>r.RentDate);


        }
    }
}
