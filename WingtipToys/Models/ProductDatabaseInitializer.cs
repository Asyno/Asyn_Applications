using System.Data.Entity;
using System.Collections.Generic;
using System.Diagnostics;

namespace WingtipToys.Models
{
    public class ProductDatabaseInitializer : DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            GetCategories().ForEach(c => context.Categories.Add(c));
            GetProducts().ForEach(p => context.Products.Add(p));
            context.SaveChanges();
            Debug.WriteLine("___________________________");
            Debug.WriteLine("records added...");
            Debug.WriteLine("");
            Debug.WriteLine("");
            Debug.WriteLine("____________________________");
        }

        private static List<Category> GetCategories()
        {
            var categories = new List<Category>
            { 
                new Category
                {
                    CategoryID = 1,
                    CategoryName = "Cars"
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "Planes"
                },
                new Category
                {
                    CategoryID = 3,
                    CategoryName = "Trucks"
                },
                new Category
                {
                    CategoryID = 4,
                    CategoryName = "Boats"
                },
                new Category
                {
                    CategoryID = 5,
                    CategoryName = "Rockets"
                }
            };

            return categories;
        }

        private static List<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    ProductID = 1,
                    ProductName = "Convertible Car",
                    Description = "This convertible car is fast! The engine is powered by a neutrino based battery (not included)." +
                                    "Power it up and let it go!",
                    ImagePath = "carconvert.png",
                    UnitPrice = 22.50,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 2,
                    ProductName = "Old-time Car",
                    Description = "There's nothing old about this toy car, expect it's looks. Compatible with other old toy cars.",
                    ImagePath = "carearly.png",
                    UnitPrice = 15.95,
                    CategoryID = 1
                }
            };

            return products;
        }
    }
}