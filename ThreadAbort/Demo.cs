using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadAbort
{
    public class Demo
    {
        public void TestMethod()
        {
            try
            {
                Console.WriteLine("Sek.-Thread gestartet.");

                // die Schleife zwingt Thread eine länger dauernde Ausführung auf
                for(int i=0;i<=100;i++)
                {
                    Console.WriteLine("Sek.-Thread-Zähler = {0}", i);
                    Thread.Sleep(50);
                }
            }
            catch(ThreadAbortException ex) { Console.WriteLine("Sek.-Thread/im Catch-Block"); }
            finally { Console.WriteLine("Sek.-Thread/in Finally"); }

            Console.WriteLine("Sek.-Thread/nach Finally");
            for(int i = 0; i <= 20; i++)
            {
                Console.WriteLine(".");
                Thread.Sleep(50);
            }
        }
    }
}
