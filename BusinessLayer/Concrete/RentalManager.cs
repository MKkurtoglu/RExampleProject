using BusinessLayer.Abstract;
using Base.Utilities.Results;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Aspects.Autofac.Validation;
using BusinessLayer.ValidationRules.FluentValidation;

namespace BusinessLayer.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        public IResult Delete(Rental entity)
        {

            

_rentalDal.Delete(entity)            ;
            return new SuccessResult();
        }

        public IDataResult<Rental> Get(int id)
        {
            var result = _rentalDal.Get(r=>r.RentalId== id);
            
            return new SuccessDataResult<Rental>(result);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            var result = _rentalDal.GetAll(r=>r.ReturnDate!=null ||r.Status==true);
            
            return new SuccessDataResult<List<Rental>>();
        }

        public IDataResult<RentalDetailDto> GetAllRentalDetails(int id)
        {
            var result = _rentalDal.GetRentalDetail(r=>r.CustomerId==id);
            return new SuccessDataResult<RentalDetailDto>(result);
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Insert(Rental entity)
        {
            if (entity.Status==true || entity.CarId!=null ||entity.ReturnDate!=null)
            {
                _rentalDal.Add(entity);
            }
            
            return new SuccessResult();
        }


        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental entity)
        {
            if (entity.Status == true || entity.CarId != null || entity.ReturnDate != null)
            {
                _rentalDal.Update(entity);
            }

            return new SuccessResult();
        }
    }
}
