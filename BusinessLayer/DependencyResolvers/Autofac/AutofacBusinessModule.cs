﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Base.Utilities.Interceptors;
using Base.Utilities.Security.JWT;
using BusinessLayer.Abstract;
using BusinessLayer.BusinessHelper;
using BusinessLayer.Concrete;
using Castle.DynamicProxy;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
            builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();

            builder.RegisterType<CarManager>().As<ICarService>().SingleInstance();
            builder.RegisterType<EfCarDal>().As<ICarDal>().SingleInstance();

            builder.RegisterType<ColorManager>().As<IColorService>().SingleInstance();
            builder.RegisterType<EfColorDal>().As<IColorDal>().SingleInstance();

            builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>().SingleInstance();

            builder.RegisterType<RentalManager>().As<IRentalService>().SingleInstance();
            builder.RegisterType<EfRentalDal>().As<IRentalDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<CarImageManager>().As<ICarImageService>();
            builder.RegisterType<EfCarImageDal>().As<ICarImageDal>();

            builder.RegisterType<ProfileImageManager>().As<IProfileImageService>();
            builder.RegisterType<EfProfileImageDal>().As<IProfileImageDal>();

            builder.RegisterType<VerifyCodeHelper>().As<IVerifyHelper>();
            builder.RegisterType<PaySystemManager>().As<PaySystemService>();
            builder.RegisterType<EfCardDal>().As<ICardDal>();
            builder.RegisterType<CodeHelper>().As<ICodeHelper>();
           








            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); // assembly getirri hangi assembly peki ? içerisinde çalışan elemanların oldğu amssbly döner..

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() //çalışan uygulamaların olduğu assmebly içerisinde implemente edilmiş interfaceleri bul
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector() // genel mantık ile yukarıdaki map classlarımız da aspectleri var mı diyue bakıyor bu sistem..
                }).SingleInstance();
        }
    }
}
