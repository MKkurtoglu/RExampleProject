using Base.EntitiesBase;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.ValidationAbstract
{
    public interface IGenericVal<T> where T : class,IEntity,new()
    {
        bool IsValue(T entity);
        bool IsLength(T entity);
    }
}
