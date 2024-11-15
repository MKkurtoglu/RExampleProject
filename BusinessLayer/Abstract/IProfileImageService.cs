using Base.BusinessBase;
using Base.Utilities.Results;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProfileImageService :IGenericService<ProfileImage>
    {
        IResult AddProfileImage( IFormFile formFile);
        IResult UpdateImage( ProfileImage profileImage,IFormFile formFile);
        IDataResult<ProfileImage> GetAllImageByUser();
    }
}
