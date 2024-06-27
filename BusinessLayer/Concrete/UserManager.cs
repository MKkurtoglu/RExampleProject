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
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal= userDal;
        }
        public IResult Delete(User entity)
        {
            _userDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<User> Get(int id)
        {
            var result = _userDal.Get(u=>u.UserId== id);
            return new SuccessDataResult<User>(result);
        }

        public IDataResult<List<User>> GetAll()
        {
            var result = _userDal.GetAll();
            return new SuccessDataResult<List<User>>(result);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Insert(User entity)
        {
            _userDal.Add(entity);
            return new SuccessResult();
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User entity)
        {
            _userDal.Update(entity);
            return new SuccessResult();
        }
    }
}
