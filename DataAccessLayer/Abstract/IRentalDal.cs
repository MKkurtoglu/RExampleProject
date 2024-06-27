using Base.DataAccessBase;
using Base.DataAccessBase.EfWorkBase;
using DataAccessLayer.Concrete;
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
    public interface IRentalDal : IGenericDal<Rental>
    {
        RentalDetailDto GetRentalDetail(Expression<Func<RentalDetailDto, bool>> filter);
    }
}
