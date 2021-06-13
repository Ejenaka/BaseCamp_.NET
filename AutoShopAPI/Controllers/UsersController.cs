using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoShop.API.Requests.Cars;
using AutoShop.API.Requests.Users;
using AutoShop.API.Responses.Cars;
using AutoShop.API.Responses.Users;
using AutoShop.Core.Interfaces;
using AutoShop.Core.Interfaces.Repositories;
using AutoShop.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop.API.Controllers
{
    /// <summary>
    /// The controller allows managing users
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<Car> _carValidator;

        public UsersController
        (
            IRepositoryManager repositoryManager, 
            IMapper mapper, 
            IValidator<User> validator, 
            IValidator<Car> carValidator
        )
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _userValidator = validator;
            _carValidator = carValidator;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Users</returns>
        [HttpGet]
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repositoryManager.Users.GetAll();
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid ID");
            }

            var foundUser = await _repositoryManager.Users.Get(id);

            if (foundUser == null)
            {
                return BadRequest("User not found");
            }

            return Ok(foundUser);
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>Created user</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateRequest request)
        {
            var model = _mapper.Map<User>(request);

            var validationResult = await _userValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            await _repositoryManager.Users.Add(model);

            var response = _mapper.Map<UserCreateResponse>(model);

            return Ok(response);
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="request">Request model</param>
        /// <returns>Updated user</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserUpdateRequest request)
        {
            if (id < 1)
            {
                return BadRequest("Invalid ID");
            }

            var foundUser = await _repositoryManager.Users.Get(id);
            
            if (foundUser == null)
            {
                return BadRequest("User is not found");
            }

            _mapper.Map(request, foundUser);

            var validation = await _userValidator.ValidateAsync(foundUser);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            
            await _repositoryManager.Users.Update(foundUser);

            var response = _mapper.Map<UserUpdateResponse>(foundUser);

            return Ok(response);
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid ID");
            }

            var foundUser = await _repositoryManager.Users.Get(id);

            if (foundUser == null)
            {
                return BadRequest("User is not found");
            }

            await _repositoryManager.Users.Delete(foundUser);

            return Ok();
        }

        /// <summary>
        /// Get user's cars
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User's cars</returns>
        [HttpGet]
        [Route("{id}/cars")]
        public async Task<IActionResult> GetCarsByUser(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid User ID");
            }

            var foundUser = await _repositoryManager.Users.Get(id);

            if (foundUser == null)
            {
                return NotFound("User doesn't exist");
            }

            var cars = await _repositoryManager.Users.GetCarsByUser(foundUser);
            var response = cars.Select(c => _mapper.Map<CarCreateResponse>(c));
            
            return Ok(response);
        }
        
        /// <summary>
        /// Creates new car for user
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="request">Request model</param>
        /// <returns>User's cars</returns>
        [HttpPost]
        [Route("{id}/cars")]
        public async Task<IActionResult> AddCarForUser(int id, [FromBody] CarCreateRequest request)
        {
            if (id < 1)
            {
                return BadRequest("Invalid User ID");
            }

            var foundUser = await _repositoryManager.Users.Get(id);

            if (foundUser == null)
            {
                return NotFound("User doesn't exist");
            }

            var model = _mapper.Map<Car>(request);
            
            var validationResult = await _carValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _repositoryManager.Users.CreateCarForUser(foundUser, model);

            var cars = await _repositoryManager.Users.GetCarsByUser(foundUser);
            var response = cars.Select(c => _mapper.Map<CarCreateResponse>(c));
            
            return Ok(response);
        }
    }
}
