using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSicherheit
{
    public class Demo
    {
        private int value;

        public void Worker()
        {
            while (true)
            {
                // Sperre aktivieren
                // Mit TryEnter wird ein rückgabewert gegeben, falls der bereich bereits gesperrt ist
                Monitor.Enter(this);
                value++;
                if (value > 100) break;
                Console.WriteLine("Zahl = {0,5} Thread = {1,3}", value, Thread.CurrentThread.GetHashCode().ToString());
                Thread.Sleep(5);

                // Sperre aufheben
                Monitor.Exit(this);
            }
        }
    }
}
