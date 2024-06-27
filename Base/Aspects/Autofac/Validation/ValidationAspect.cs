using Base.CrossCuttingConcerns.ValidationTools;
using Base.Utilities.Interceptors;
using Castle.DynamicProxy;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;

        // bu bir attribute olduğu için ctor ile biz typeof yaparak bu duruma özel olarak validator type yı çekiyoruz.

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("bu bir validation type değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation) // parametre bizim attribute ile etkiledğimiz methottur
        {
            var validator = (IValidator) Activator.CreateInstance(_validatorType);

            var entity = _validatorType.BaseType.GetGenericArguments()[0];


            // methottan parametre birden fazla gelebilir bu yüzden valdiation base classındaki class türü ile eşleşenleri seçeceğiz.

            var sameClass = invocation.Arguments.Where(i=>i.GetType()==entity);

            foreach ( var entityofsameclass in sameClass )
            {
                ValidationTool.ValidateMethod(validator, entityofsameclass);
            }
        }
    }
}
