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
        public IEnumerable<Car> Get()
        {
            return _repository.GetAll();
        }

        // GET: Сars/1
        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            return _repository.Get(id);
        }

        // POST: Сars
        [HttpPost]
        public ActionResult AddCar([FromBody]Car car)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(car);
                return Ok();
            }

            return BadRequest();
        }

        // PUT: Сars/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, Car car)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            _repository.Update(id, car);

            return Ok();
        }

        // DELETE: Сars/1
        [HttpDelete("{id}")]
        public ActionResult<Car> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            _repository.Delete(id);

            return Ok();
        }
    }
}
