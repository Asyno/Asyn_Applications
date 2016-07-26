using System;
using System.Threading;

namespace First_Thread
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadStart del = new ThreadStart(TestMethod);
            Thread thread = new Thread(del);

            // den zweiten Thread starten
            thread.Start();

            for(int i = 0; i <= 100; i++)
            {
                for (int k = 1; k <= 20; k++)
                    Console.Write("X");
                Console.WriteLine("Primär-Thread " + i);
            }
            Console.ReadLine();
        }

        // diese Methode wird in einem eigenen Thread ausgeführt
        public static void TestMethod()
        {
            for(int i = 0; i <= 100; i++)
            {
                for (int k = 1; k <= 20; k++)
                    Console.Write(".");
                Console.WriteLine("Sekundär-Thread " + i);
            }
        }
    }
}
