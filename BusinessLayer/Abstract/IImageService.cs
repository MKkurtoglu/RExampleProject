using Base.BusinessBase;
using Base.DataAccessBase;
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
    public interface ICarImageService: IGenericService<CarImage>
    {
        IResult AddCarImage(int carId,IFormFile formFile);
        IResult UpdateImage(CarImage entity,IFormFile formFile);
        IDataResult<List<CarImage>> GetAllImageByCar(int carId);
    }
}
