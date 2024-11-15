using Base.CrossCuttingConcerns.Caching;
using Base.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessHelper
{
    public  class VerifyCodeHelper : IVerifyHelper
    {

       ICacheManager _cacheManager;
        public  VerifyCodeHelper()
        {
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public bool VerifyCode( string enteredCode)
        {
            var methodName = $"{typeof(ICodeHelper).FullName}.GenerateVerificationCode";
            var key = $"{methodName}(6)";
            var cachedCode = _cacheManager.Get<string>(key);
            if (cachedCode != null && cachedCode == enteredCode)
            {
                _cacheManager.Remove(key); // Kod doğrulandıktan sonra cache'den sil
                return true;
            }
            _cacheManager.Remove(key);
            return false;
        }
    }
}
