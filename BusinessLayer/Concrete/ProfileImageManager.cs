using Base.Extensions;
using Base.Utilities.Business;
using Base.Utilities.ImageHelper;
using Base.Utilities.Results;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing; // Resim işleme için
using SixLabors.ImageSharp.Formats;    // Farklı formatları desteklemek için
namespace BusinessLayer.Concrete
{
    public class ProfileImageManager : IProfileImageService
    {
        string guid;
        IProfileImageDal _profileImageDal;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProfileImageManager(IProfileImageDal profileImageDal, IHttpContextAccessor httpContextAccessor)
        {
            _profileImageDal = profileImageDal;
            _httpContextAccessor = httpContextAccessor;
        }
        public IResult AddProfileImage(IFormFile formFile)
        {
           

            var checkAllowExtension = CheckAllowExtension(guid);
            var checkMİME = CheckMIME(formFile);
            IResult[] metot = new[] { checkAllowExtension, checkMİME };



            BusinessRule.Run(metot);
            {
                var id = _httpContextAccessor.HttpContext.User.FindId();
                var userId = int.Parse(id);


                ProfileGuidHelper.CreaterProfileGuid(formFile, out guid);
                EditImageSize(formFile,guid);
                var profile = new ProfileImage();
                profile.UserId = userId;
                profile.Url = guid;
                profile.UploadedAt = DateTime.Now;
                _profileImageDal.Add(profile);
                var data = _profileImageDal.Get(c => c.UserId == userId);
                if (data != null)
                {

                    return new SuccessResult();
                }
                else { return new ErrorResult(); }
            }


        }

        public IResult Delete(ProfileImage entity)
        {
            _profileImageDal.Delete(entity);
            ProfileGuidHelper.Delete(entity.Url);
            return new SuccessResult();
        }

        public IDataResult<ProfileImage> Get(int id)
        {
            var data = _profileImageDal.Get(r => r.Id == id);
            return new SuccessDataResult<ProfileImage>(data);
        }

        public IDataResult<List<ProfileImage>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<ProfileImage> GetAllImageByUser()
        {
            var id = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(id);
            var data = _profileImageDal.Get(r => r.UserId == userId);
            return new SuccessDataResult<ProfileImage>(data);
        }

        public IResult Insert(ProfileImage entity)
        {
            throw new NotImplementedException();
        }

        public IResult Update(ProfileImage entity)
        {
            throw new NotImplementedException();
        }

        public IResult UpdateImage(ProfileImage profileImage, IFormFile formFile)
        {
            
            var checkAllowExtension = CheckAllowExtension(profileImage.Url);
            var checkMİME = CheckMIME(formFile);
            IResult[] metot = new[] { checkAllowExtension, checkMİME };



            BusinessRule.Run(metot);
            {

               

                ProfileGuidHelper.Update(formFile, profileImage.Url!,out guid);
                EditImageSize(formFile, guid);
                profileImage.UploadedAt = DateTime.Now;
                _profileImageDal.Update(profileImage);
                return new SuccessResult();
            }
        }


        private IResult CheckAllowExtension(string extension)
        {
            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(extension))
            {
                return new ErrorResult("Invalid image format. Only JPG and PNG are allowed.");
            }
            return new SuccessResult();
        }

        private IResult CheckMIME(IFormFile formFile)
        {
            if (formFile.ContentType != "image/jpeg" && formFile.ContentType != "image/png")
            {
                return new ErrorResult("Invalid image format. Only JPG and PNG are allowed.");
            }
            return new SuccessResult();
        }


        private void EditImageSize(IFormFile formFile ,string guid)
        {
            using (var image = Image.Load(formFile.OpenReadStream()))
            {
                // Resmi yeniden boyutlandır (örnek olarak 300x300 piksel)
                int width = 300;
                int height = 300;

                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(width, height),
                    Mode = ResizeMode.Crop // Resmi kırparak yeniden boyutlandır
                }));

                 // Kaydedilecek dosya yolu

                // Resmi diske kaydediyoruz
                image.Save(guid);
                //image.Save(guid);
            }
        }
    }
}
