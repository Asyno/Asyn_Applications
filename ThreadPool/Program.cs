using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            // den Threadpool erforschen
            int maxThreads;
            int asyncThreads;
            System.Threading.ThreadPool.GetMaxThreads(out maxThreads, out asyncThreads);
            Console.WriteLine("Max. Anzahl Threads: {0}", maxThreads);
            Console.WriteLine("Max. Anzahl E/A-Threads: {0}", asyncThreads);

            // Benachrichtigungsereignis, Zustand 'nicht signalisieren'
            AutoResetEvent ready = new AutoResetEvent(false);

            // Anforder eines threads aus dem Pool
            System.Threading.ThreadPool.QueueUserWorkItem(new WaitCallback(Calculate), ready);
            Console.WriteLine("Der Hauptthread wartet ...");

            // Hauptthread in den Wartezustand setzen
            ready.WaitOne();

            Console.WriteLine("Sekundärthread ist fertig.");
            Thread.Sleep(5000);
        }

        public static void Calculate(object obj)
        {
            Console.WriteLine("Im Sekundärthread");
            Thread.Sleep(5000);

            // Ereigniszustand auf 'signalisieren' festlegen
            ((AutoResetEvent)obj).Set();
        }
    }
}
