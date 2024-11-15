using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfCardDal : ICardDal
    {
        public Card GetCustomerInfoCard(Expression<Func<Card, bool>> filter = null)
        {
            using (RExampleProjectContext projectContext = new RExampleProjectContext())
            {
                var result = filter==null?null:projectContext.Cards.Where(filter).ToList();
                return result.FirstOrDefault();
            }
        }
    }
}
