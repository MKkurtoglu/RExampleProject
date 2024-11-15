using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        IColorService _colorService;
        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }
        [HttpGet("get")]
        public IActionResult Get(int id)
        {
            var result = _colorService.Get(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _colorService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);

            }
            return BadRequest();
        }
        [HttpPost("addColor")]
        public IActionResult AddColor(Color color)
        {
            var result = _colorService.Insert(color);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("deleteColor")]
        public IActionResult DeleteColor(int id)
        {
            var result = _colorService.Get(id);
            var result2 = _colorService.Delete(result.Data);
            if (result2.IsSuccess)
            {
                return Ok(result2);
            }
            return BadRequest();
        }
        [HttpPost("updateColor")]
        public IActionResult UpdateColor(Color color)
        {
            var result = _colorService.Update(color);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
