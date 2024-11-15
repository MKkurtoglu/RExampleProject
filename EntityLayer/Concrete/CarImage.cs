using Base.EntitiesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CarImage :IEntity
    {
        public int ImageId { get; set; }
        public int CarId { get; set; }
        
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }

    }
}
