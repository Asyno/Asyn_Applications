using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTPLTask
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task, parameterlos
            Task task1 = Task.Factory.StartNew(DoSomething);
            Task task2 = new Task(DoSomething);
            task2.Start();
            Task.WaitAll(task1, task2);


            // Task mit Parametern
            int[] arr = { 1, 2, 3, 4, 5 };
            Console.WriteLine("Task Start");
            Task task = Task.Factory.StartNew((liste) => {
                int[] p = (int[])liste;
                foreach (int item in arr) Console.WriteLine(item);
            }, arr);
            Console.WriteLine("Task wird ausgeführt");
            // Warten bis Task fertig ist
            task.Wait();
            Console.WriteLine("Task beendet");


            // Task mit Parameter und Rückgabe
            int value = 12;
            Task<long> task3 = Task<long>.Factory.StartNew((wert) =>
            {
                int var = (int)wert;
                Thread.Sleep(3000);
                return var * var;
            }, value);
            Console.WriteLine("Ich warte ...");
            Console.WriteLine("Result: {0}", task3.Result);


            // Task von aussen unterbrechen
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Task task4 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                while (true)
                {
                    if (token.IsCancellationRequested) { token.ThrowIfCancellationRequested(); }
                }
            }, cts.Token);
            cts.Cancel();
            Console.WriteLine("Abbruch der parallelen Operation ...");
            try { task4.Wait(); }
            catch (Exception ex) { Console.WriteLine("In catch: " + ex.InnerException.Message); }


            Console.ReadLine();
        }

        public static void DoSomething()
        {
            Console.WriteLine("Task wird ausgeführt ...");
        }
    }
}
