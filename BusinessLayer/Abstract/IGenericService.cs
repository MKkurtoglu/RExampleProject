using Base.EntitiesBase;
using Base.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.BusinessBase
{
    public interface IGenericService<T> where T : class,IEntity,new()
    {
        IDataResult<T> Get (int id);
        IDataResult<List<T> > GetAll ();
        IResult Insert(T entity);
        IResult Update (T entity);
        IResult Delete (T entity); 
    }
}
