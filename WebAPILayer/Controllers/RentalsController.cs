using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        IRentalService _rentalservice;
        public RentalsController(IRentalService rentalService)
        {
            _rentalservice = rentalService;
        }
        [HttpGet("get")]
        public ActionResult Get(int id)
        {
            var result = _rentalservice.Get(id);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            var resul= _rentalservice.GetAll();
            if(resul.IsSuccess)
            {
                return Ok(resul);
            }
            return BadRequest(resul);
        }
        [HttpGet("getAllDetails")]
        public IActionResult GetAllDetails(int id)
        {
            var result=_rentalservice.GetAllRentalDetails(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("addRental")]
        public IActionResult AddRental(Rental rental)
        {
           var result= _rentalservice.Insert(rental);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("deleteRental")]
        public IActionResult DeleteRental(int id)
        {
            var result = _rentalservice.Get(id);
            var sonuc =_rentalservice.Delete(result.Data);
            if (sonuc.IsSuccess)
            {
                return Ok(sonuc);
            }
            else
            {
                return BadRequest(sonuc.Message);
            }
        }
        [HttpPost("updateRental")]
        public IActionResult UpdateRental(Rental rental)
        {
            var result = _rentalservice.Update(rental);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
