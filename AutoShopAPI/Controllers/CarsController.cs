using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoShop.API.Requests.Cars;
using AutoShop.API.Responses.Cars;
using AutoShop.Core.Interfaces;
using AutoShop.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop.API.Controllers
{
    /// <summary>
    /// Controller allows managing the cars
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IValidator<Car> _validator;

        /// <summary>
        /// Cars controller constructor
        /// </summary>
        public CarsController(IRepositoryManager repositoryManager, IMapper mapper, IValidator<Car> validator)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Get all cars
        /// </summary>
        /// <returns>All cars</returns>
        [HttpGet]
        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _repositoryManager.Cars.GetAll();
        }

        /// <summary>
        /// Get car by id
        /// </summary>
        /// <param name="id">Car id</param>
        /// <returns>Car</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid ID");
            }

            var foundCar = await _repositoryManager.Cars.Get(id);

            return Ok(foundCar);
        }

        /// <summary>
        /// Creates new car
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>Created car</returns>
        [HttpPost]
        [Route("{userID}")]
        public async Task<IActionResult> AddCarAsync([FromBody] CarCreateRequest request)
        {
            var model = _mapper.Map<Car>(request);

            var validation = await _validator.ValidateAsync(model);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            await _repositoryManager.Cars.Add(model);

            var response = _mapper.Map<CarCreateResponse>(model);

            return Ok(response);
        }

        /// <summary>
        /// Updates car
        /// </summary>
        /// <param name="id">Car id</param>
        /// <param name="request">Request model</param>
        /// <returns>Updated car</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCarAsync(int id, CarUpdateRequest request)
        {
            if (id < 1)
            {
                return BadRequest("Invalid ID");
            }

            var foundCar = await _repositoryManager.Cars.Get(id);

            if (foundCar == null)
            {
                return BadRequest("Car not found");
            }

            _mapper.Map(request, foundCar);

            var validation = await _validator.ValidateAsync(foundCar);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            
            await _repositoryManager.Cars.Update(foundCar);

            var response = _mapper.Map<CarUpdateResponse>(foundCar);

            return Ok(response);
        }

        /// <summary>
        /// Deletes car
        /// </summary>
        /// <param name="id">Car id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundCar = await _repositoryManager.Cars.Get(id);

            if (foundCar == null)
            {
                return BadRequest("Car not found");
            }

            await _repositoryManager.Cars.Delete(foundCar);

            return Ok();
        }
    }
}
