﻿using Base.EntitiesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ProfileImage :IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Url { get; set; }  // Profil resmi URL
        public DateTime UploadedAt { get; set; }  // Yüklenme tarihi
    }
}
