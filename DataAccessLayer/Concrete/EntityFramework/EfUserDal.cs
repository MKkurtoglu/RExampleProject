using Base.DataAccessBase.EfWorkBase;
using Base.EntitiesBase.Concrete;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfUserDal : EfGenericRepositoryDal<User,RExampleProjectContext>,IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new RExampleProjectContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public UserProfileDto GetProfileDto(int userId)
        {
            using (var context = new RExampleProjectContext())
            {
                var userProfile = (from u in context.Users
                                   join ui in context.ProfileImages on u.Id equals ui.UserId into userImages
                                   from ui in userImages.DefaultIfEmpty()  // Sol dış join
                                   where u.Id == userId
                                   select new UserProfileDto
                                   {
                                       Id = u.Id,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName,
                                       Email = u.Email,
                                       ProfileImageUrl = ui.Url
                                   }).FirstOrDefault();

                return userProfile;
            }
        }
    }
}
