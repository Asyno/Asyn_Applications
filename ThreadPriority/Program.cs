using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPriority
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo obj = new Demo();
            Thread thread1, thread2;
            thread1 = new Thread(new ThreadStart(obj.Execution1));
            thread2 = new Thread(new ThreadStart(obj.Execution2));

            // die Priorität von thread1 hochstzen
            thread1.Priority = System.Threading.ThreadPriority.AboveNormal;

            // thread 1 starten
            thread1.Start();

            // thread 2 starten
            thread2.Start();

            Thread.Sleep(5000);
        }
    }
}
