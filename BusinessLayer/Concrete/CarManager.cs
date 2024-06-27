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
namespace BusinessLayer.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        public IResult Delete(Car entity)
        {
            _carDal.Delete(entity);
          return new SuccessResult("Silme işlemi başarılı bir şekilde gerçekleşmiştir.");
        }

        public IDataResult<Car >Get(int id)
        {
           return new SuccessDataResult<Car>(_carDal.Get(p=>p.CarId == id),"Üürn başarılı bir şekilde database'den çekilmiştir..");
        }

        public IDataResult<List<Car> >GetAll()
        {
           return new SuccessDataResult<List<Car>>( _carDal.GetAll(),"Tüm ürünler listelenmiştir.");
        }

        public IDataResult<List<CarDetailDto> >GetAllCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>> (_carDal.GetCarDetail(),"Ürün detayları getirilmiştir.");
        }

        public IDataResult< List<Car> > GetCarsByBrandId(int brandId)
        {
           return new SuccessDataResult<List<Car>> (_carDal.GetAll(p=>p.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>> (_carDal.GetAll(c=>c.ColorId == colorId));
        }
        [ValidationAspect(typeof(CarValidator))]
        public IResult Insert(Car entity)
        {
            
                
                _carDal.Add(entity);
                return new SuccessResult("Ürün başarılı bir şekilde eklenmiştir.");
            
                  
        }
        [ValidationAspect(typeof(CarValidator))]

        public IResult Update(Car entity)
        {
            

                _carDal.Update(entity);
                return new SuccessResult("Ürün başarılı bir şekilde güncellenmiştir..");
            
        }

       
    }
}
