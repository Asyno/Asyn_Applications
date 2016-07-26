using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDB_test
{
    class Program
    {
        static void Main(string[] args)
        {

            Database.SetInitializer(new BlogContextSeedInitializer());
            using (var db = new BlogContext())
            {
                db.Categories.ForEachAsync(c => Console.WriteLine(c.Name));
                Console.WriteLine(db.Database.Connection.ConnectionString);
            }
            Console.ReadLine();
        }
    }
}
