using System;
using System.Collections.Generic;
using System.Text;
using HotelFinder.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelFinder.DataAccess
{
    public class HotelDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-SK8NDD6\\SQLEXPRESS;Database=HotelDb;uid=sa;pwd=123456;");

        }

        public DbSet<Hotel> Hotels { get; set; }


    }
}
