using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadMonitorWaitPulse
{
    class Program
    {
        public static bool finished = false;
        public static bool thread1Waiting = false;
        public static bool thread2Waiting = false;

        static void Main(string[] args)
        {
            MyNumber zahl = new MyNumber();
            ProduceNumber prod = new ProduceNumber(zahl);
            ConsumeNumber cons = new ConsumeNumber(zahl);
            Thread thread1, thread2;

            // Threads instanzieren
            thread1 = new Thread(new ThreadStart(prod.MakeNumber));
            thread2 = new Thread(new ThreadStart(cons.GetNumber));

            // Threads starten
            thread1.Start();
            thread2.Start();
            Console.ReadLine();
        }
    }
}
