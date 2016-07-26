using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTPLParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            // Aufruf von Parallel Invoke mit drei Methoden
            //Parallel.Invoke(Task1, Task2, Task3);

            // Aufruf von Parallel Invoke mit drei Lambda-Ausdrücken
            Parallel.Invoke(() => { for (int i = 0; i < 10; i++) { Thread.Sleep(50); Console.Write(" #1 "); } },
                            () => { for (int i = 0; i < 10; i++) { Thread.Sleep(50); Console.Write(" #2 "); } },
                            () => { for (int i = 0; i < 10; i++) { Thread.Sleep(50); Console.Write(" #3 "); } });

            Console.WriteLine("Done");


            // Aufruf von Parallel For
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ParallelTest();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);

            watch.Start();
            SynchTest();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);

            // Aufruf von Parallel ForEach
            string[] namen = { "Peter", "Uwe", "Udo", "Willi", "Pia", "Michael", "Conie" };
            Parallel.ForEach(namen, name =>
            {
                Console.WriteLine(name);
            });


            Console.ReadLine();
        }

        static void Task1()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(50);
                Console.Write(" #1 ");
            }
        }

        static void Task2()
        { 
            for(int i = 0; i < 10; i++)
            {
                Thread.Sleep(50);
                Console.Write(" #2 ");
            }
        }

        static void Task3()
        {
            for(int i = 0; i < 10; i++)
            {
                Thread.Sleep(50);
                Console.Write(" #3 ");
            }
        }

        static void SynchTest()
        {
            double[] arr = new double[1000000];
            for (int i = 0; i < 1000000; i++)
                arr[i] =Math.Pow(i,0.333)*Math.Sqrt(Math.Sin(i));
        }

        static void ParallelTest()
        {
            double[] arr = new double[1000000];
            // Parallel default
            Parallel.For(0, 1000000, i =>
                    { arr[i] = Math.Pow(i, 0.333) * Math.Sqrt(Math.Sin(i)); });

            // Parallel mit stop/break option
            // Stop beendet die ausführung, Break führt die Schleife noch einmal bis zu ende durch
            /* Parallel.For(0,1000000, (i, option) =>
            {
                arr[i] = Math.Pow(i, 0.333) * Math.Sqrt(Math.Sin(i));
                if ( i > 1000) option.Stop();
            }*/
        }
    }
}
