using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadAsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            DoSomethingAsync();
            for (int i = 0; i < 1000; i++)
                Console.Write(".");
            Console.ReadLine();
        }

        static async void DoSomethingAsync()
        {
            Console.Write("Start");
            Console.Write(await TestAsync());
        }

        static async Task<string> TestAsync()
        {
            await Task.Delay(20);
            return "Fertig";
        }
    }
}
