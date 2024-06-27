using Base.DataAccessBase.EfWorkBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfRentalDal : EfGenericRepositoryDal<Rental,RExampleProjectContext>,IRentalDal
    {
        
       public RentalDetailDto GetRentalDetail(Expression<Func<RentalDetailDto, bool>> filter)
        {
            using (RExampleProjectContext projectContext = new RExampleProjectContext())
            {
                var result = from r in projectContext.Rentals
                             join c in projectContext.Customers

                             on r.CustomerId equals c.CustomerId

                             join u in projectContext.Users

                             on c.UserId equals u.UserId
                             join b in projectContext.Cars
                             on r.CarId equals b.CarId
                             join br in projectContext.Brands
                             on b.BrandId equals br.BrandId


                             select new RentalDetailDto {CustomerId=c.CustomerId, FirsName = u.FirsName, LastName = u.LastName, DailyPrice = b.DailyPrice, BrandName = br.BrandName, RentDate = r.RentDate, ReturnDate = r.ReturnDate };
                return result.FirstOrDefault(filter) ;
            }
        }
    }
}
