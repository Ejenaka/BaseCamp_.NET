using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoShop.API.Requests.Users;
using AutoShop.API.Responses;
using AutoShop.Core.Enums;
using AutoShop.Core.Interfaces.Configurations;
using AutoShop.Core.Interfaces.Managers;
using AutoShop.Core.Interfaces.Repositories;
using AutoShop.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AutoShop.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenConfiguration _tokenConfiguration;
        private readonly IValidator<UserLoginModel> _loginValidator;
        private readonly IValidator<User> _userValidator;
        private readonly IMapper _mapper;

        public AuthController
        (
            IUserRepository repository, 
            IAuthenticationManager authenticationManager, 
            IPasswordHasher<User> passwordHasher, 
            ITokenConfiguration tokenConfiguration, 
            IValidator<UserLoginModel> loginValidator,
            IMapper mapper, 
            IValidator<User> userValidator
        )
        {
            _repository = repository;
            _authenticationManager = authenticationManager;
            _passwordHasher = passwordHasher;
            _tokenConfiguration = tokenConfiguration;
            _loginValidator = loginValidator;
            _mapper = mapper;
            _userValidator = userValidator;
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginModel">Login model</param>
        /// <returns>Token</returns>
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginModel loginModel)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginModel);

            if (!validationResult.IsValid)
            {
                BadRequest(validationResult.Errors);
            }

            var user = await _repository.GetUserByLogin(loginModel.Username);

            if (user == null)
            {
                return NotFound($"User with login '{loginModel.Username}' doesn't exists");
            }

            var result = GenerateToken(user);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(result);
            var response = new TokenResponse
            {
                UserId = user.ID,
                ExpiresIn = result.ValidTo,
                Token = tokenString
            };

            return Ok(response);
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="request">Model for creating user</param>
        /// <returns>Token</returns>
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserCreateRequest request)
        {
            var model = _mapper.Map<User>(request);
            
            var validationResult = await _userValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var userByLogin = await _repository.GetUserByLogin(model.Login);
            
            if (userByLogin != null)
            {
                return BadRequest("User with this login already exists");
            }

            var userByEmail = await _repository.GetUserByEmail(model.Email);
            
            if (userByEmail != null)
            {
                return BadRequest("User with this email already exists");
            }

            var loginModel = new UserLoginModel {Username = model.Login, Password = model.Password};

            model.Password = _passwordHasher.HashPassword(model, model.Password);
            model.CreatedDate = DateTime.UtcNow;
            model.Role = UserRoleEnum.User;

            await _repository.Add(model);

            var user = _authenticationManager.Authenticate(model, loginModel);
            
            if (user == null)
            {
                return Unauthorized();
            }

            var token = GenerateToken(user);
            var response = new TokenResponse
            {
                UserId = model.ID,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = token.ValidTo
            };

            return Ok(response);
        }

        private JwtSecurityToken GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
                new(ClaimTypes.Role, user.Role.ToString().ToLower())
            };
            

            return new JwtSecurityToken(
                _tokenConfiguration.Issuer,
                _tokenConfiguration.Audience,
                claims,
                expires: DateTime.Now.AddDays(_tokenConfiguration.TokenExpires),
                signingCredentials: credentials);
        }
    }
}