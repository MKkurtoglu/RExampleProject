﻿using Azure.Core;
using Base.EntitiesBase.Concrete;
using Base.Utilities.Business;
using Base.Utilities.Results;
using Base.Utilities.Security.Hashing;
using Base.Utilities.Security.JWT;
using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using EntitiesLayer.DTOs;
using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Insert(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email).IsSuccess == true)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<Base.Utilities.Security.JWT.AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<Base.Utilities.Security.JWT.AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IResult UpdateUser(UserForUpdateDto userForUpdateDto)
        {
            IResult checkNullResult = CheckNull<UserForUpdateDto>(userForUpdateDto);
            IResult[] metot = new[] { checkNullResult }; // Diziyi doğru tanımla

            var resultRun = BusinessRule.Run(metot);
            if (resultRun.IsSuccess != false)
            {
 var user = _userService.GetById();
            if (user != null)
            {
                user.Data.FirstName = userForUpdateDto.FirstName;
                user.Data.LastName = userForUpdateDto.LastName;
                user.Data.Email = userForUpdateDto.Email;
                var result=_userService.Update(user.Data);
                if (result.IsSuccess)
                {
return new SuccessResult("Kullanıcı Güncellendi");
                }
                return new ErrorResult("Kullanıcı Güncellenme İşlemi Başarısız");
            }
            return new ErrorResult("Kullanıcı Güncellenme İşlemi Başarısız");
            }
           return resultRun;
        }

        // gelen verinin null olup olmadığını kontrol etme

       private IResult CheckNull<T>(T value)
        {
            if (value != null)
            {
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }
    }
}
