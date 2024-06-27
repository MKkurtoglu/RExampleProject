using BusinessLayer.Abstract;
using Base.Utilities.Results;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Aspects.Autofac.Validation;
using BusinessLayer.ValidationRules.FluentValidation;

namespace BusinessLayer.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }
        public IResult Delete(Color entity)
        {
            _colorDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<Color> Get(int id)
        {
            

                var sonuc = _colorDal.Get(c=>c.ColorId==id);
                if (sonuc != null)
                {
                    return new SuccessDataResult<Color>(sonuc,"işlem başarılı şekilde gerçekleşmiştir.");
                }
               

            
          else { return new ErrorDataResult<Color>("işlem başarısızdır"); }
           
        }

        public IDataResult<List<Color>> GetAll()
        {
            var sonuc = _colorDal.GetAll();
            if (sonuc != null)
            {
                return new SuccessDataResult<List<Color>>(sonuc, "işlem başarılı şekilde gerçekleşmiştir.");
            }



            else { return new ErrorDataResult<List<Color>>("işlem başarısızdır"); }
        }
        [ValidationAspect(typeof(ColorValidator))]

        public IResult Insert(Color entity)
        {
            _colorDal.Add(entity);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(ColorValidator))]

        public IResult Update(Color entity)
        {
            _colorDal.Update(entity);
            return new SuccessResult();
        }
    }
}
