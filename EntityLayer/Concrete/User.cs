using Base.EntitiesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User : IEntity
    {
        //Kullanıcılar tablosu oluşturunuz. Users-->Id,FirstName,LastName,Email,Password
        public int UserId { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
