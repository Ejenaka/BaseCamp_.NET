using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShop.Core.Models;
using FluentValidation;

namespace AutoShop.API.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Login).NotEmpty().Length(3, 100);
            RuleFor(u => u.Password).Length(8, 100);
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.FirstName).Length(0, 100);
            RuleFor(u => u.LastName).Length(0, 100);
        }
    }
}
