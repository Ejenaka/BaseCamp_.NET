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
        private readonly CarRepository _carDB = new CarRepository();
        private readonly ILogger<Car> _logger;

        public CarsController(ILogger<Car> logger)
        {
            _logger = logger;
        }

        // GET: api/<СarsController>
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            return _carDB.GetAll();
        }

        // GET api/<СarsController>/5
        [HttpGet("{id}")]
        public Car Get(int id)
        {
            return _carDB.Get(id);
        }

        // POST api/<СarsController>
        [HttpPost]
        public void AddCar([FromBody]Car car)
        {
            if (ModelState.IsValid)
            {
                _carDB.Add(car);
            }
        }

        // PUT api/<СarsController>/5
        [HttpPut("{id}")]
        public void Put(int id, Car car)
        {
        }

        // DELETE api/<СarsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _carDB.Delete(id);
        }
    }
}
