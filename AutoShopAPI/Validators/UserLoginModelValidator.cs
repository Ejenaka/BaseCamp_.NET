using AutoShop.Core.Models;
using FluentValidation;

namespace AutoShop.API.Validators
{
    public class UserLoginModelValidator : AbstractValidator<UserLoginModel>
    {
        public UserLoginModelValidator()
        {
            RuleFor(u => u.Username).Length(3, 100);
            RuleFor(u => u.Password).Length(8, 100);
        }
    }
}