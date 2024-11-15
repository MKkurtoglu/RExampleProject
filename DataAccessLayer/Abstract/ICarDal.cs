using Base.DataAccessBase;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICarDal :IGenericDal<Car>
    {
        List<CarDetail2Dto> GetAllCarByColorname(Expression<Func<CarDetail2Dto, bool>> filter = null);
        List<CarDetail2Dto> GetCarDetail(Expression<Func<CarDetail2Dto, bool>> filter = null);
        List<CarDetail2Dto> GetCarDetail2();
        List<CarImageDto> GetAllCarWithImage(Expression<Func<CarImageDto, bool>> filter = null);
        CarImageDto GetCarWithByCarId(Expression<Func<CarImageDto, bool>> filter = null);


    }
}
