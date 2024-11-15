using BusinessLayer.Abstract;

using Base.Utilities.Results;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Aspects.Autofac.Validation;

using BusinessLayer.ValidationRules.FluentValidation;
using Base.Aspects.Autofac.Cache;
using Base.Utilities.Business;
using Base.EntitiesBase.Concrete;
using static System.Net.Mime.MediaTypeNames;
using BusinessLayer.BusinessAspects.Autofac;
namespace BusinessLayer.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        ICarImageService _carImageService;
        public CarManager(ICarDal carDal, ICarImageService carImageService)
        {
            _carDal = carDal;
            _carImageService = carImageService;
        }
        [SecuredOperation("admin")]
        public IResult Delete(Car entity)
        {
            var result = BusinessRule.Run(DefaultDelete(entity.CarId));
            if (result.IsSuccess)
            {
                _carDal.Delete(entity);
                return new SuccessResult("Silme işlemi başarılı bir şekilde gerçekleşmiştir.");
            }
            else
            {
                return new ErrorResult();
            }
        }

        public IDataResult<Car> Get(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(p => p.CarId == id), "Üürn başarılı bir şekilde database'den çekilmiştir..");
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), "Tüm ürünler listelenmiştir.");
        }

        public IDataResult<List<CarDetail2Dto>> GetAllCarDetails(string brandName)
        {
            return new SuccessDataResult<List<CarDetail2Dto>>(_carDal.GetCarDetail(c => c.BrandName == brandName), "Ürün detayları getirilmiştir.");
        }

        public IDataResult<List<CarDetail2Dto>> GetAllCarDetails2()
        {
            return new SuccessDataResult<List<CarDetail2Dto>>(_carDal.GetCarDetail2());
        }

        public IDataResult<List<CarDetail2Dto>> GetAllCarsByColorName(string colorName)
        {
            var result = _carDal.GetAllCarByColorname(c => c.ColorName == colorName);
            return new SuccessDataResult<List<CarDetail2Dto>>(result);
        }
        
        public IDataResult<List<CarImageDto>> GetAllCarWithImage(string brandName)
        {

            var result = _carDal.GetAllCarWithImage(c => c.BrandName == brandName );
            Default(result);
            return new SuccessDataResult<List<CarImageDto>>(result);


        }

        private static void Default(List<CarImageDto> result)
        {
            string defaultImagePath = @"C:\Users\kurto\Source\Repos\RExampleProject\WebAPILayer\www.root\images\Linkedin[1].jpg";

            foreach (var item in result)
            {
                if (item.ImagePaths == null || !item.ImagePaths.Any())
                {
                    item.ImagePaths = new List<string> { defaultImagePath };
                }
            }
        }

        public IDataResult<List<CarImageDto>> GetAllCarWithImage2()
        {
            var result = _carDal.GetAllCarWithImage(c=>c.IsRented==false);
            string defaultImagePath = @"C:\Users\kurto\Source\Repos\RExampleProject\WebAPILayer\www.root\images\Linkedin[1].jpg";

            foreach (var item in result)
            {
                if (item.ImagePaths == null || !item.ImagePaths.Any())
                {
                    item.ImagePaths = new List<string> { defaultImagePath };
                }
            }
            return new SuccessDataResult<List<CarImageDto>>(result);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }


        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Insert(Car entity)
        {


            _carDal.Add(entity);
            return new SuccessResult("Ürün başarılı bir şekilde eklenmiştir.");


        }
        [ValidationAspect(typeof(CarValidator))]
        [SecuredOperation("admin")]

        public IResult Update(Car entity)
        {


            _carDal.Update(entity);
            return new SuccessResult("Ürün başarılı bir şekilde güncellenmiştir..");

        }

        private IResult CheckEmptyPath(string brandName)
        {
            var result = _carDal.GetAllCarWithImage(c => c.BrandName == brandName);
            string defaultImagePath = @"C:\Users\kurto\Source\Repos\RExampleProject\WebAPILayer\wwwroot\images\Linkedin[1].jpg";

            foreach (var item in result)
            {
                if (item.ImagePaths == null || !item.ImagePaths.Any())
                {
                    item.ImagePaths = new List<string> { defaultImagePath };
                }
            }

            return new SuccessResult();
        }
        private IResult DefaultDelete(int carId)
        {
            var data = _carImageService.GetAllImageByCar(carId);
            foreach (var item in data.Data)
            {
                _carImageService.Delete(item);
            }
            return new SuccessResult();
        }

        public IDataResult<CarImageDto> GetCarWithById(int carId)
        {
            var data = _carDal.GetCarWithByCarId(c=>c.CarId==carId);
            return new SuccessDataResult<CarImageDto>(data);    
        }
    }

}
