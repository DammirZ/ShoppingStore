using Microsoft.EntityFrameworkCore;
using SamsungAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsungAPI.Data
{

    public class SeedData
    {

        public static void SeedDatabase(DataContext context)
        {

            context.Database.Migrate();

            if (context.Products.Count() == 0)
            {
                var c1 = new Category
                {
                    Name = "Category 1",
                  
                };
                var c2 = new Category
                {
                    Name = "Category 2",
                    
                };
                var c3 = new Category
                {
                    Name = "Category 3",
              
                };

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Description = "A boat for one person",
                        Category = c1,
                        Price = 275,
                        Ratings = new List<Rating> {
                             new Rating { Stars = 4 }, new Rating { Stars = 3 }}
                    },
                     new Product
                     {
                         Name = "Lifejacket",
                         Description = "Protective and fashionable",
                         Category = c1,
                         Price = 48.95m,
                         Ratings = new List<Rating> {
                             new Rating { Stars = 2 }, new Rating { Stars = 5 }}
                     },
                     new Product
                     {
                         Name = "Soccer Ball",
                         Description = "FIFA-approved size and weight",
                         Category = c2,
                         Price = 19.50m,
                         Ratings = new List<Rating> {
                             new Rating { Stars = 1 }, new Rating { Stars = 3 }}
                     },
                     new Product
                     {
                         Name = "Corner Flags",
                         Description = "Give your pitch a professional touch",
                         Category = c2,
                         Price = 34.95m,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Stadium",
                         Description = "Flat-packed 35,000-seat stadium",
                         Category = c2,
                         Price = 79500,
                         Ratings = new List<Rating> { new Rating { Stars = 1 },
                             new Rating { Stars = 4 }, new Rating { Stars = 3 }}
                     },
                     new Product
                     {
                         Name = "Thinking Cap",
                         Description = "Improve brain efficiency by 75%",
                         Category = c3,
                         Price = 16,
                         Ratings = new List<Rating> { new Rating { Stars = 5 },
                             new Rating { Stars = 4 }}
                     },
                     new Product
                     {
                         Name = "Unsteady Chair",
                         Description = "Secretly give your opponent a disadvantage",
                         Category = c3,
                         Price = 29.95m,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Human Chess Board",
                         Description = "A fun game for the family",
                         Category = c3,
                         Price = 75,
                     },
                     new Product
                     {
                         Name = "Bling-Bling King",
                         Description = "Gold-plated, diamond-studded King",
                         Category = c3,
                         Price = 1200,
                     });
                context.SaveChanges();
            }
        }
    }
}
