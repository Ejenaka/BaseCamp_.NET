using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using AutoShopAPI.Models;
using AutoShopAPI.Repositories;

namespace AutoShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IRepository<Car> _repository;
        private readonly ILogger<Car> _logger;

        public CarsController(IRepository<Car> repository, ILogger<Car> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: Сars
        [HttpGet]
        public async Task<IEnumerable<Car>> Get()
        {
            return await _repository.GetAll();
        }

        // GET: Сars/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            return await _repository.Get(id);
        }

        // POST: Сars
        [HttpPost]
        public async Task<ActionResult> AddCar([FromBody]Car car)
        {
            if (ModelState.IsValid)
            {
                await _repository.Add(car);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        // PUT: Сars/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Car car)
        {
            if (id < 1)
            {
                ModelState.AddModelError("ID", "ID must be greater 0");
            }

            if (ModelState.IsValid)
            {
                await _repository.Update(id, car);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        // DELETE: Сars/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("ID", "ID must be greater 0");
            }

            if (ModelState.IsValid)
            {
                await _repository.Delete(id);
                return Ok();
            }
            
            return BadRequest(ModelState);
        }
    }
}
