using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utilities.Results
{
    public class Result : IResult
    {
        //voidlerde kullanılacak bir geri dönüş değeridir.
        public Result(bool ısSuccess,string message) :this(ısSuccess)
        {
            
            Message = message;
        }
        public Result(bool ısSuccess)
        {
            IsSuccess = ısSuccess;
        }
        public bool IsSuccess { get; }
        public string Message {  get; }
    }
    // kullanım kolaylığı açısından success ve errorresult kullanılabilir.
}
