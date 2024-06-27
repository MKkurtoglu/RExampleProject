using Base.BusinessBase;
using Base.Utilities.Results;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICarService : IGenericService<Car>
    {
        
        IDataResult <List<Car>>GetCarsByBrandId(int brandId);
       IDataResult< List<Car> >GetCarsByColorId(int colorId);
       IDataResult< List<CarDetailDto> >GetAllCarDetails();
    }
}
