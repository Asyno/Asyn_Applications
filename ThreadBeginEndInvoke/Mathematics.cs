using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadBeginEndInvoke
{
    class Mathematics
    {
        private delegate int CalculateHandler(int x);
        CalculateHandler del;

        // Methode Calculate wird synchron ausgeführt
        public int Calculate(int x)
        {
            Console.WriteLine("--Bearbeitung starten---");
            for(int i = 0; i <= 20; i++)
            {
                Console.Write(".X.");
                Thread.Sleep(10);
            }
            Console.WriteLine("---Bearbeiten beendet---");
            return x * x;
        }

        // Starten der asynchronen Ausführung
        public IAsyncResult BeginCalculate(int intVar, AsyncCallback callback, object state)
        {
            del = new CalculateHandler(Calculate);

            // aufruf der Methode Calculate, die in einem eigenen
            // Thread ausgeführt wird
            return del.BeginInvoke(intVar, callback, state);
        }

        // Beenden der asynchornen Ausführung
        public int EndCalculate(IAsyncResult ar)
        {
            return del.EndInvoke(ar);
        }
    }
}
