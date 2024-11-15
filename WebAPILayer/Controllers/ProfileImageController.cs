using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileImageController : ControllerBase
    {
        IProfileImageService _profileImageService;
        public ProfileImageController(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }


        [HttpGet("getProfileImage")]
        public IActionResult GetProfileImage()
        {
            var result = _profileImageService.GetAllImageByUser();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addProfileImage")]
        public IActionResult AddProfileImage([FromForm] IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("File cannot be null");
            }
            var result = _profileImageService.AddProfileImage(file);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("updateProfileImage")]
        public IActionResult UpdateProfileImage([FromForm] int id, [FromForm] IFormFile file)
        {
            var result = _profileImageService.Get(id);
            if (result.Data == null)
            {
                return NotFound($"Profile image with id {id} not found.");
            }

            var result2 = _profileImageService.UpdateImage(result.Data, file);
            if (result2.IsSuccess)
            {
                return Ok(result2);
            }
            return BadRequest(result2);
        }


        [HttpPost("deleteCarImage")]
        public IActionResult DeleteProfileImage()
        {
            var result = _profileImageService.GetAllImageByUser();
            var result2 = _profileImageService.Delete(result.Data);
            if (result2.IsSuccess)
            {
                return Ok(result2);
            }
            return BadRequest(result2);

        }
    }
}
