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

                             on c.UserId equals u.Id
                             join b in projectContext.Cars
                             on r.CarId equals b.CarId
                             join br in projectContext.Brands
                             on b.BrandId equals br.BrandId


                             select new RentalDetailDto {CustomerId=c.CustomerId, FirsName = u.FirstName, LastName = u.LastName, DailyPrice = b.DailyPrice, BrandName = br.BrandName, RentDate = r.RentDate, ReturnDate = r.ReturnDate };
                return result.FirstOrDefault(filter) ;
            }
        }

        public List<RentalDetail2Dto> GetRentals()
        {
            using (RExampleProjectContext projectContext = new RExampleProjectContext())
            {
                var result = from r in projectContext.Rentals
                             join c in projectContext.Customers

                             on r.CustomerId equals c.CustomerId

                             join u in projectContext.Users

                             on c.UserId equals u.Id
                             join b in projectContext.Cars
                             on r.CarId equals b.CarId
                             join br in projectContext.Brands
                             on b.BrandId equals br.BrandId


                             select new RentalDetail2Dto { RentalId=r.RentalId, CustomerFullName = u.FirstName+u.LastName,CarName=br.BrandName, RentDate=r.RentDate,ReturnDate=r.ReturnDate,Status=r.Status};
                return result.ToList();
            }
        }
    }
}
