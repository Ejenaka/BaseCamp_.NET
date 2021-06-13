using System;
using AutoShop.Core.Models;
using AutoShop.Core.Interfaces.Managers;
using Microsoft.AspNetCore.Identity;

namespace AutoShop.Core.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthenticationManager(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        
        public User Authenticate(User user, UserLoginModel loginModel)
        {
            var usernameMatches = string.Equals(loginModel.Username, user.Login, StringComparison.InvariantCultureIgnoreCase);
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, loginModel.Password);
            var passwordMatches = passwordVerificationResult == PasswordVerificationResult.Success;

            if (usernameMatches && passwordMatches)
            {
                return user;
            }

            return null;
        }
    }
}