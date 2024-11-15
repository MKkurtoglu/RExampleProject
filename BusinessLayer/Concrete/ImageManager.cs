using Base.EntitiesBase;
using Base.Utilities.Business;
using Base.Utilities.ImageHelper;
using Base.Utilities.Results;
using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        string guid;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult AddCarImage(int carId, IFormFile formFile)
        {
            //IResult? result = BusinessRule.Run(CountByCarId(carId));
            //if (result != null)
            //{
            //    return result;
            //}
            

            
            GuidHelper.CreaterGuid(formFile, out guid);
            var carImage = new CarImage();
            carImage.CarId = carId;
            carImage.ImagePath = guid;  
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            var data=_carImageDal.Get(c=>c.CarId==carId);
            if (data != null)
            {

                return new SuccessResult();
            }
            else { return new ErrorResult(); }
        }

        public IResult Delete(CarImage entity)
        {
            _carImageDal.Delete(entity);
            GuidHelper.Delete(entity.ImagePath);
            return new SuccessResult();   
        }

        public IDataResult<CarImage> Get(int id)
        {
            var data = _carImageDal.Get(c => c.ImageId == id);
            return new SuccessDataResult<CarImage>(data);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IResult Insert(CarImage entity)
        {
            throw new NotImplementedException();
        }

        private IResult CountByCarId(int id)
        {
            if (_carImageDal.GetAll(c => c.CarId == id).Count >= 5)
            {
                return new ErrorResult(Messages.ImageLimitExceded);
            }
            else
            {
                return new SuccessResult();
            }
        }

        public IResult Update(CarImage entity)
        {
            throw new Exception();
        }

        public IResult UpdateImage(CarImage entity, IFormFile formFile)
        {

            GuidHelper.Update(formFile, entity.ImagePath!);
            entity.Date = DateTime.Now;
            _carImageDal.Update(entity);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAllImageByCar(int carId)
        {
            var result = _carImageDal.GetAll(i=>i.CarId == carId);
            return new SuccessDataResult<List<CarImage>>(result);
        }
    }
}
