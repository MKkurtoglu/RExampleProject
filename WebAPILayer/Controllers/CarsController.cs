using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarService _carservice;
        public CarsController(ICarService carService)
        {
            _carservice = carService;
        }
        [HttpGet("getCarByBrand")]
        public IActionResult GetCarByBrand(int id) {
        var result = _carservice.GetCarsByBrandId(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("getCarByColor")]
        public IActionResult GetCarByColor(int id)
        {
            var result = _carservice.GetCarsByColorId(id);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("getDetails")]
        public IActionResult GetDetails()
        {
            var result = _carservice.GetAllCarDetails();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _carservice.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else { return BadRequest(result); }
        }
        [HttpPost("addCar")]
        public IActionResult AddCar(Car car)
        {
            var result= _carservice.Insert(car);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("deleteCar")]
        public IActionResult DeleteCar(int id)
        {
            var result = _carservice.Get(id);
            var result2 = _carservice.Delete(result.Data);
            if (result2.IsSuccess)
            {
                return Ok(result2);
            }
            return BadRequest(result2);
        }
    }
}
