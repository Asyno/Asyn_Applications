using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSicherheit
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo obj = new Demo();
            Thread thread1, thread2;
            thread1 = new Thread(new ThreadStart(obj.Worker));
            thread2 = new Thread(new ThreadStart(obj.Worker));
            thread1.Start();
            thread2.Start();
            Console.ReadLine();
        }
    }
}
