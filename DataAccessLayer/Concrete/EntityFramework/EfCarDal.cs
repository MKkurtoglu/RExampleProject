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
    public class EfCarDal : EfGenericRepositoryDal<Car, RExampleProjectContext>, ICarDal
    {
        public List<CarDetail2Dto> GetAllCarByColorname(Expression<Func<CarDetail2Dto, bool>> filter = null)
        {
            using (RExampleProjectContext projectContext = new RExampleProjectContext())
            {

                var result = from c in projectContext.Cars
                             join b in projectContext.Brands

                             on c.BrandId equals b.BrandId
                             join o in projectContext.Colors
                             on c.ColorId equals o.ColorId
                             select new CarDetail2Dto { ModelYear = c.ModelYear, BrandName = b.BrandName, ColorName = o.ColorName, DailyPrice = c.DailyPrice };
                return result.Where(filter).ToList();
            }
        }

        //public List<CarImageDto> GetAllCarWithImage(Expression<Func<CarImageDto, bool>> filter = null)
        //{
        //    using (RExampleProjectContext projectContext = new RExampleProjectContext())
        //    {

        //        var result = from c in projectContext.Cars
        //                     join b in projectContext.Brands

        //                     on c.BrandId equals b.BrandId
        //                     join o in projectContext.Colors

                             

        //                     on c.ColorId equals o.ColorId
        //                     join ci in projectContext.CarImages on c.CarId equals ci.CarId into carImages
        //                     select new CarImageDto { CarId=c.CarId,ModelYear = c.ModelYear, BrandName = b.BrandName, ColorName = o.ColorName, DailyPrice = c.DailyPrice , ImagePaths = carImages.Select(ci => ci.ImagePath).Take(5).ToList() };
        //        return filter == null ? result.ToList() : result.Where(filter).ToList();
        //    }
        //}

        public List<CarImageDto> GetAllCarWithImage(Expression<Func<CarImageDto, bool>> filter = null)
        {
            using (RExampleProjectContext projectContext = new RExampleProjectContext())
            {
                var result = from c in projectContext.Cars
                             join b in projectContext.Brands on c.BrandId equals b.BrandId
                             join o in projectContext.Colors on c.ColorId equals o.ColorId
                             join ci in projectContext.CarImages on c.CarId equals ci.CarId into carImages
                            
                             join r in projectContext.Rentals on c.CarId equals r.CarId into carRentals
                             from rental in carRentals.DefaultIfEmpty() // Eğer bir kiralama kaydı yoksa null olabilir.

                             select new CarImageDto
                             {
                                 CarId = c.CarId,
                                 ModelYear = c.ModelYear,
                                 BrandName = b.BrandName,
                                 ColorName = o.ColorName,
                                 Description=c.Description,
                                 DailyPrice = c.DailyPrice,
                                 ImagePaths = carImages.Select(ci => ci.ImagePath).Take(5).ToList(),
                                 //IsRented = rental == null || rental.ReturnDate != null ? false : true // Kirada olup olmadığını kontrol ediyoruz.
                                 IsRented = rental != null || rental.ReturnDate != null ? true : false || rental.Status==true // Kirada olup olmadığını kontrol ediyoruz.
                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }

        public List<CarDetail2Dto> GetCarDetail(Expression<Func<CarDetail2Dto, bool>> filter = null)
        {
              
            
            using (RExampleProjectContext projectContext = new RExampleProjectContext())
            {

                var result = from c in projectContext.Cars
                             join b in projectContext.Brands
                             
                             on c.BrandId equals b.BrandId
                             join o in projectContext.Colors
                             on c.ColorId equals o.ColorId
                             select new CarDetail2Dto { ModelYear=c.ModelYear,BrandName=b.BrandName,ColorName=o.ColorName,DailyPrice=c.DailyPrice};
                return result.Where(filter).ToList();
            }
        }

        public List<CarDetail2Dto> GetCarDetail2()
        {
            using (RExampleProjectContext projectContext = new RExampleProjectContext())
            {

                var result = from c in projectContext.Cars
                             join b in projectContext.Brands

                             on c.BrandId equals b.BrandId
                             join o in projectContext.Colors
                             on c.ColorId equals o.ColorId
                             select new CarDetail2Dto {  BrandName = b.BrandName, ColorName = o.ColorName, DailyPrice = c.DailyPrice ,ModelYear=c.ModelYear };
                return result.ToList();
            }
        }

        public CarImageDto GetCarWithByCarId(Expression<Func<CarImageDto, bool>> filter = null)
        {
            using (RExampleProjectContext projectContext = new RExampleProjectContext())
            {
                var result = from c in projectContext.Cars
                             join b in projectContext.Brands on c.BrandId equals b.BrandId
                             join o in projectContext.Colors on c.ColorId equals o.ColorId
                             join ci in projectContext.CarImages on c.CarId equals ci.CarId into carImages

                             join r in projectContext.Rentals on c.CarId equals r.CarId into carRentals
                             from rental in carRentals.DefaultIfEmpty() // Eğer bir kiralama kaydı yoksa null olabilir.

                             select new CarImageDto
                             {
                                 CarId = c.CarId,
                                 ModelYear = c.ModelYear,
                                 BrandName = b.BrandName,
                                 ColorName = o.ColorName,
                                 Description = c.Description,
                                 DailyPrice = c.DailyPrice,
                                 ImagePaths = carImages ==null? null : carImages.Select(ci => ci.ImagePath).Take(5).ToList(),
                                 //IsRented = rental == null || rental.ReturnDate != null ? false : true // Kirada olup olmadığını kontrol ediyoruz.
                                 IsRented = rental != null || rental.ReturnDate != null ? true : false || rental.Status == true // Kirada olup olmadığını kontrol ediyoruz.
                             };

                return filter == null ? null : result.Where(filter).First();
            }
        }
    }
}
