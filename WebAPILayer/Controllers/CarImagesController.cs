using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;
        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpPost("addCarImage")]
        public IActionResult AddCarImage([FromForm] IFormFile file, [FromForm] string id)
        {
            var carId= int.Parse(id);
            //var result=  _carImageService.AddCarImage(carId, file);
            //  if (result.IsSuccess == true)
            //  {
            //      return Ok(result);
            //  }
            //  return BadRequest(result);
            if (file == null)
            {
                return BadRequest("File cannot be null");
            }

            var result = _carImageService.AddCarImage(carId, file);
            if (result == null)
            {
                return BadRequest("Result cannot be null");
            }

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("getAllImageByCar")]
        public IActionResult GetAllImageByCar(int carId)
        {
            var result = _carImageService.GetAllImageByCar(carId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("getAllCarImage")]
        public IActionResult GetAllCarImage()
        {
            var result=_carImageService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deleteCarImage")]
        public IActionResult DeleteCarImage( int imageId)
        {
            var result = _carImageService.Get(imageId);
          var result2=  _carImageService.Delete(result.Data);
            if (result2.IsSuccess)
            {
                return Ok(result2);
            }
            return BadRequest(result2);

        }

        [HttpPost("updateCarImage")]
        public IActionResult UpdateCarImage([FromForm] IFormFile formFile, [FromForm] string id)
        {
            var imageId= int.Parse(id);
            var result = _carImageService.Get(imageId);
         var result2 =   _carImageService.UpdateImage(result.Data,formFile);
            if (result2.IsSuccess)
            {
                return Ok(result2);
            }
            return BadRequest(result2);
        }
    }
}
