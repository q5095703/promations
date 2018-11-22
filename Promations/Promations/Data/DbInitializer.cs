using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Promations.Models;

namespace Promations.Data
{
    public class DbInitializer
    {
        public static void Initialize(DBContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var Products = new List<Product>()
                {
                   new Product { Name = "item2", IsDeleted = false},
                   new Product { Name = "item3",  IsDeleted = false},
                    new Product { Name = "itemtwo",  IsDeleted = true}
            };
            foreach (Product s in Products)
            {
                context.Products.Add(s);
            }
            context.SaveChanges();

            var Promations = new Promation[]
            {
             new Promation {Start=null, End=null, ProductID=1,  IsDeleted = false},
              new Promation {Start=null, End=null, ProductID=3,  IsDeleted = false}

            };
            foreach (Promation c in Promations)
            {
                context.Promations.Add(c);
            }
            context.SaveChanges();

        }
    }
}
