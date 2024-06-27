using BusinessLayer.Abstract;
using Base.Utilities.Results;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.ValidationRules.FluentValidation;
using FluentValidation;
using Base.CrossCuttingConcerns.ValidationTools;
using Base.Aspects.Autofac.Validation;

namespace BusinessLayer.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Delete(Brand entity)
        {
            _brandDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<Brand> Get(int id)
        {
            var result = _brandDal.Get(b=>b.BrandId == id);
            return new SuccessDataResult<Brand>(result);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            var result = _brandDal.GetAll();
            return new SuccessDataResult<List<Brand>>(result);
        }
        [ValidationAspect(typeof(BrandValidator))]

        public IResult Insert(Brand entity)
        {
            //var context = new ValidationContext<Brand>(entity);
            //BrandValidator validator = new BrandValidator();
            //var result = validator.Validate(context);
            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}

            
            _brandDal.Add(entity);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand entity)
        {
            //ValidationTool.ValidateMethod(new BrandValidator(), entity);

            _brandDal.Update(entity);
            return new SuccessResult();
        }
    }
}
