using Base.Aspects.Autofac.Cache;
using Base.CrossCuttingConcerns.Caching;
using Base.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessHelper
{
    public  class CodeHelper : ICodeHelper
    {
        ICacheManager _cacheManager;
        public CodeHelper()
        {
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();

        }
        //[CacheAspect(duration: 2)]
        public  string GenerateVerificationCode(int length = 6)
        {
            var methodName = $"{typeof(ICodeHelper).FullName}.GenerateVerificationCode";
            var key = $"{methodName}(6)";
            Random random = new Random();
            string code = string.Empty;
            for (int i = 0; i < length; i++)
            {
                code += random.Next(0, 10).ToString();
            }
            _cacheManager.Add(key, code, 2);
            return code;
        }
    }
}
