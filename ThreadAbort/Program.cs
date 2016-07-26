using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadAbort
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo obj = new Demo();
            Thread thread = new Thread(new ThreadStart(obj.TestMethod));
            Console.WriteLine("Thread wird jetzt gestartet");

            // Sekundärthread starten
            thread.Start();
            Console.WriteLine("Thread ist gestartet");
            Thread.Sleep(200);

            // ser sekundäre Thread wird surch den Primärthread mit
            // der Methode Abort zerstört
            thread.Abort();
            Thread.Sleep(200);

            if (thread.IsAlive)
                Console.WriteLine("Der Sek.-Thread lebt noch");
            else
                Console.WriteLine("Der Sek.-Thread ist aufgegeben");
            Thread.Sleep(5000);
        }
    }
}
