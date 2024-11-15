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

        [HttpGet("getCarByCarId")]
        public IActionResult GetCarByCarId(int id)
        {
            var result = _carservice.GetCarWithById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getCarByColor")]
        public IActionResult GetCarByColor(int id)
        {
            var result = _carservice.GetCarsByColorId(id);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getDetailsByBrand")]
        public IActionResult GetDetails(string brandName)
        {
            var result = _carservice.GetAllCarDetails(brandName);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("getAllCarWithImage")]
        public IActionResult GetAllCarWithImage(string brandName)
        {
            var result = _carservice.GetAllCarWithImage(brandName);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("getAllCarWithImagee")]
        public IActionResult GetAllCarWithImage()
        {
            var result = _carservice.GetAllCarWithImage2();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("getAllCarByColorName")]
        public IActionResult GetAllCarByColorname(string colorName)
        {
            var result = _carservice.GetAllCarsByColorName(colorName);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("getDetails2")]
        public IActionResult GetDetails2()
        {
            var result = _carservice.GetAllCarDetails2();
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
        [HttpDelete("deleteCar")]
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

        [HttpPost("updateCar")]
        public IActionResult UpdateCar(Car car)
        {
            var result = _carservice.Update(car);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
