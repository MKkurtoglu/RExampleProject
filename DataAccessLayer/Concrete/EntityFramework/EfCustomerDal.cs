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
    public class EfCustomerDal : EfGenericRepositoryDal<Customer, RExampleProjectContext>, ICustomerDal
    {
        public List<CustomerDto> GetCustomerDetails()
        {
            using (RExampleProjectContext context = new RExampleProjectContext())
            {
                var data = from c in context.Customers
                           join u in context.Users
                           on c.UserId equals u.Id

                           select new CustomerDto { CustomerId = c.CustomerId, CustomerName = u.FirstName + u.LastName, CustomerCompanyName = c.CompanyName };
                return data.ToList();
                           
            }
        }
    }
}
