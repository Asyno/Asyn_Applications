using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDB_test
{
    class BlogContextSeedInitializer : DropCreateDatabaseAlways<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            Category cat1 = new Category { Id = Guid.NewGuid(), Name = ".NET Framework" };
            Category cat2 = new Category { Id = Guid.NewGuid(), Name = "SQL Server" };
            Category cat3 = new Category { Id = Guid.NewGuid(), Name = "jQuery" };
            context.Categories.Add(cat1);
            context.Categories.Add(cat2);
            context.Categories.Add(cat3);

            int i = context.SaveChanges();
            Console.WriteLine("{0} records added...", i);
        }
    }
}
