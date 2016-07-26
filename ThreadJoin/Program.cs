using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadAbort;

namespace ThreadJoin
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo obj = new Demo();
            Thread TheThread = new Thread(new ThreadStart(obj.TestMethod));
            Console.WriteLine("Thread wird jetzt gestartet");

            // sekundären Thread starten
            TheThread.Start();
            Console.WriteLine("Thread ist gestartet");
            Console.WriteLine("Vor Abort..............");

            // der sekundäre Thread wird durch den Primärthread mit
            // der Methode Abort zerstört
            Thread.Sleep(200);
            TheThread.Abort();
            TheThread.Join();

            // die folgende Anweisung simuliert Code, der vom Beenden
            // des Sekundärthreads abhängig ist
            Console.WriteLine("nach Abort.............");
            Thread.Sleep(100);

            if (TheThread.IsAlive)
                Console.WriteLine("Der Sek.-Thread lebt noch");
            else
                Console.WriteLine("Der Sek.-Thread ist aufgegeben");
            Thread.Sleep(5000);
        }
    }
}
