using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using AutoShop.Core.Models;

namespace AutoShop.API.Validators
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.Brand).NotEmpty();
            RuleFor(c => c.Model).NotEmpty();
            RuleFor(c => c.Year).InclusiveBetween(1800, DateTime.Now.Year);
            RuleFor(c => c.EngineVolume).InclusiveBetween(0.5, 100.0);
            RuleFor(c => c.Mileage).InclusiveBetween(0, int.MaxValue);
        }
    }
}
