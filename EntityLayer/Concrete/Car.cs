﻿
using Base.EntitiesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    
    public class Car : IEntity
    {
        //Id, BrandId, ColorId, ModelYear, DailyPrice, Description
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int ModelYear { get; set; }
        public int DailyPrice { get; set; }
        public string Description { get; set; }
        


    }
}
