using Base.DataAccessBase.EfWorkBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfCarDal : EfGenericRepositoryDal<Car, RExampleProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetail()
        {
              
            
            using (RExampleProjectContext projectContext = new RExampleProjectContext())
            {

                var result = from c in projectContext.Cars
                             join b in projectContext.Brands
                             
                             on c.BrandId equals b.BrandId
                             join o in projectContext.Colors
                             on c.ColorId equals o.ColorId
                             select new CarDetailDto { CarName =c.Description,BrandName=b.BrandName,ColorName=o.ColorName,DailyPrice=c.DailyPrice};
                return result.ToList();
            }
        }
    }
}
