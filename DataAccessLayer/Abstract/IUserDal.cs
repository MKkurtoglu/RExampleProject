using Base.DataAccessBase;
using Base.EntitiesBase.Concrete;
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
    public interface IUserDal : IGenericDal<User>
    {
        List<OperationClaim> GetClaims(User user);
        UserProfileDto GetProfileDto(int userId);
    }
}
