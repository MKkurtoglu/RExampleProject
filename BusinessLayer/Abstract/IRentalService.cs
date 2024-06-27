using Base.BusinessBase;
using Base.Utilities.Results;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRentalService : IGenericService<Rental>
    {
        IDataResult<RentalDetailDto> GetAllRentalDetails(int id);
    }
}
