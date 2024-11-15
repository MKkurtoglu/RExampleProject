﻿using Base.EntitiesBase.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class RExampleProjectContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CarImage sınıfında ImageId birincil anahtar olarak belirlenmiştir
            modelBuilder.Entity<CarImage>().HasKey(ci => ci.ImageId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=DESKTOP-F2H9TMP;database=DbRExample;integrated security=true;Trusted_Connection=True;encrypt=false;");
        }
      public  DbSet<Car> Cars { get; set; }
       public DbSet<Color> Colors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Card> Cards{ get; set; }
        public DbSet<ProfileImage> ProfileImages{ get; set; }
    }
}
