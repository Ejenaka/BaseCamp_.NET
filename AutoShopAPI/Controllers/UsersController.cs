using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoShop.API.Requests.Users;
using AutoShop.API.Responses.Users;
using AutoShop.API.Validators;
using AutoShop.Core.Interfaces;
using AutoShop.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoShop.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;

        public UsersController(IRepositoryManager repositoryManager, IMapper mapper, IValidator<User> validator)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _validator = validator;
        }

        // GET
        [HttpGet]
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repositoryManager.Users.GetAll();
        }

        // GET ALL
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

        // POST
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateRequest request)
        {
            var model = _mapper.Map<User>(request);

            var validationResult = await _validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            await _repositoryManager.Users.Add(model);

            var response = _mapper.Map<UserCreateResponse>(model);

            return Ok(response);
        }

        // PUT
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

            var validation = await _validator.ValidateAsync(foundUser);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            
            await _repositoryManager.Users.Update(foundUser);

            var response = _mapper.Map<UserUpdateResponse>(foundUser);

            return Ok(response);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest("Indalid ID");
            }

            var foundUser = await _repositoryManager.Users.Get(id);

            if (foundUser == null)
            {
                return BadRequest("User is not found");
            }

            await _repositoryManager.Users.Delete(foundUser);

            return Ok();
        }
    }
}
