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
using Base.EntitiesBase.Concrete;
using BusinessLayer.BusinessAspects.Autofac;
using Base.Utilities.Security.JWT;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Base.Extensions;

namespace BusinessLayer.Concrete
{

    public class UserManager : IUserService
    {
        IUserDal _userDal;
        ITokenHelper _tokenHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserManager(IUserDal userDal,ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor)
        {
            _userDal= userDal;
            _tokenHelper= tokenHelper;
            _httpContextAccessor= httpContextAccessor;
        }
        public IResult Delete(User entity)
        {
            _userDal.Delete(entity);
            return new SuccessResult();
        }
        [SecuredOperation("admin,customer")]
        public IDataResult<User> Get(int id)
        {
            var ıd=_tokenHelper.GetId();
            var userId = int.Parse(ıd);
            var result = _userDal.Get(u=>u.Id== userId);
            return new SuccessDataResult<User>(result);
        }



        public IDataResult<List<User>> GetAll()
        {
            var result = _userDal.GetAll();
            return new SuccessDataResult<List<User>>(result);
        }

        
        public IResult Insert(User entity)
        {
            _userDal.Add(entity);
            return new SuccessResult();
        }

        
        public IResult Update(User entity)
        {
            _userDal.Update(entity);
            return new SuccessResult();
        }
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {

            var result = _userDal.GetClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(result);
        }
        public IDataResult<User> GetByMail(string email)
        {
            var result = _userDal.Get(u => u.Email == email);
            if (result==null)
            {
                return new ErrorDataResult<User>(result);
            }
            return new SuccessDataResult<User>(result);
        }
        [SecuredOperation("admin,customer")]
        public IDataResult<UserProfileDto> GetDto()
        {
            var id = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(id);
            var result=_userDal.GetProfileDto(userId);
            if (result == null) 
            {
            return new ErrorDataResult<UserProfileDto>("Kullanıcı Bilgileri Bulunamadı");
            }
            return new SuccessDataResult<UserProfileDto>("Kullanıcı Bilgileri Alındı");
        }

        public IDataResult<User> GetById()
        {
            var ıd = _tokenHelper.GetId();
            var userId = int.Parse(ıd);
            var result = _userDal.Get(u => u.Id == userId);
            return new SuccessDataResult<User>(result);
        }
    }
}
