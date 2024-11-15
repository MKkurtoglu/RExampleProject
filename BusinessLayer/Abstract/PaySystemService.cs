using Base.Utilities.Results;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface PaySystemService
    {
       IResult CheckAndPay(Card card);
        IResult VerifyCode(string enteredCode);
    }
}
