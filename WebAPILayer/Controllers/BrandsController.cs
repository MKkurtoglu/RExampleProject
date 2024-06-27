using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        IBrandService _brandService;
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet("get")]
        public ActionResult Get(int id)
        {
            var result = _brandService.Get(id);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll();    
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("addBrand")]
        public IActionResult AddBrand(Brand brand)
        {
            var result = _brandService.Insert(brand);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("updateBrand")]
        public IActionResult UpdateBrand(Brand brand)
        {
            var result = _brandService.Update(brand);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("deleteBrand")]
        public IActionResult DeleteBrand(int id)
        {
            var result = _brandService.Get(id);
            var result2 = _brandService.Delete(result.Data);
            if(result2.IsSuccess)
            {
                return Ok(result2);
            }
            return BadRequest();
        }
    }
}
