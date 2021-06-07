using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoShop.API.Requests.Users;
using AutoShop.API.Responses.Users;
using AutoShop.Core.Interfaces;
using AutoShop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UsersController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
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
