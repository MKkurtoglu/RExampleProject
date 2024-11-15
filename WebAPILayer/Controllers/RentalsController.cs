using Base.Aspects.Autofac.Cache;
using Base.CrossCuttingConcerns.Caching;
using Base.Utilities.IoC;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        IRentalService _rentalservice;
        PaySystemService _paysystemservice;
        ICacheManager _cacheManager;
        public RentalsController(IRentalService rentalService, PaySystemService paysystemservice)
        {
            _rentalservice = rentalService;
            _paysystemservice = paysystemservice;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        [HttpGet("get")]
        public ActionResult Get(int id)
        {
            var result = _rentalservice.Get(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            var resul = _rentalservice.GetAll();
            if (resul.IsSuccess)
            {
                return Ok(resul);
            }
            return BadRequest(resul);
        }
        [HttpGet("getAllDetails")]
        public IActionResult GetAllDetails(int id)
        {
            var result = _rentalservice.GetAllRentalDetails(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("getAllDetails2")]
        public IActionResult GetAllDetails2(int id)
        {
            var result = _rentalservice.GetAllRentalDetails2();
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
        //[CacheRemoveAspect("WebAPILayer.Controllers.PaySystemController.VerifyCode(*)")]
        [CacheRemoveAspect("BusinessLayer.Concrete.PaySystemService.VerifyCode")]
        [RequireHttps]
        public IActionResult AddRental(Rental rental, [FromHeader(Name = "VerificationToken")] string verificationToken)
        {
            //var key = "BusinessLayer.Concrete.PaySystemService.VerifyCode";
            //    var value = _cacheManager.Get<string>(key);
            // value ile istediğiniz işlemi yapın
            var methodName = $"{typeof(PaySystemService).FullName}.{nameof(PaySystemService.VerifyCode)}";
            // methodName: "BusinessLayer.Concrete.PaySystemService.VerifyCode"
            //var arguments = new List<object> { enteredCode };
            var key = $"{methodName}()";
            var value = _cacheManager.Get(key);

            // Token doğrulaması yapılır.
            if (value.ToString() != verificationToken)
            {
                return BadRequest("Doğrulama token'ı geçersiz veya süresi dolmuş.");
            }
            else
            {
                var rentalResult = _rentalservice.Insert(rental);
                if (!rentalResult.IsSuccess)
                {
                    return BadRequest(rentalResult.Message);
                }

                // Başarılı işlem sonrası token silinir.


                return Ok(rentalResult);
            }



        }

        [HttpPost("deleteRental")]
        public IActionResult DeleteRental(int id)
        {
            var result = _rentalservice.Get(id);
            var sonuc = _rentalservice.Delete(result.Data);
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
