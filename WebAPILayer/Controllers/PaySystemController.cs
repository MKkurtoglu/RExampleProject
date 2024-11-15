using Base.Aspects.Autofac.Cache;
using Base.CrossCuttingConcerns.Caching;
using Base.Utilities.IoC;
using BusinessLayer.Abstract;
using BusinessLayer.BusinessHelper;
using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaySystemController : ControllerBase
    {
        PaySystemService _paySystemService;
        IVerifyHelper _verifyHelper;
        ICacheManager _cacheManager;
        public PaySystemController(PaySystemService paySystemService, IVerifyHelper verifyHelper)
        {
            _paySystemService = paySystemService;
            _verifyHelper = verifyHelper;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        [HttpPost("pay")]
        public IActionResult Pay( Card card)
        {
            var result = _paySystemService.CheckAndPay(card);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            // Kullanıcıya doğrulama kodu gönderildi.
            return Ok(result);
        }

        [HttpPost("verifyCode")]
        //[CacheAspect(duration: 1)]

        public IActionResult VerifyCode([FromBody]  string enteredCode)
        {
           
            
            var result = _paySystemService.VerifyCode(enteredCode);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            else
            {
                var methodName = $"{typeof(PaySystemService).FullName}.{nameof(PaySystemService.VerifyCode)}";
                // methodName: "BusinessLayer.Concrete.PaySystemService.VerifyCode"
                //var arguments = new List<object> { enteredCode };
                var key = $"{methodName}()";
                //// key: "BusinessLayer.Concrete.PaySystemService.VerifyCode(123456)"

                _cacheManager.Add(key, result.Message, 2);
                return Ok(result);
            }
            // Doğrulama başarılı, token oluşturuluyor.


        }
    }
}