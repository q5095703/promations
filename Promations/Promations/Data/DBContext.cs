using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Promations.Models;
using Microsoft.EntityFrameworkCore;

namespace Promations.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Promation> Promations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Promation>().ToTable("Promation");
          
        }
    }
}
