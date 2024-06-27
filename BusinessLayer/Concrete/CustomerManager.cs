using BusinessLayer.Abstract;
using Base.Utilities.Results;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Aspects.Autofac.Validation;
using BusinessLayer.ValidationRules.FluentValidation;

namespace BusinessLayer.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal= customerDal;
        }
        public IResult Delete(Customer entity)
        {
            _customerDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<Customer> Get(int id)
        {
            var result = _customerDal.Get(c => c.CustomerId == id);
            if (result == null)
            {
                return new ErrorDataResult<Customer>($"{id}'li müşteri yok ya da yanlış müşteri id'si girdiniz");
            }
            return new SuccessDataResult<Customer>(result);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            var result = _customerDal.GetAll();
            return new SuccessDataResult<List<Customer>>(result);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Insert(Customer entity)
        {
            _customerDal.Add(entity);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(CustomerValidator))]

        public IResult Update(Customer entity)
        {
            _customerDal.Update(entity);
            return new SuccessResult();
        }
    }
}
