using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringVSStringBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("test mit String");
            Stopwatch watch = new Stopwatch();
            string text = "";
            watch.Start();
            for (int i = 0; i < 50000; i++)
                text += "x";
            watch.Stop();
            Console.WriteLine("Zeit: {0}", watch.ElapsedMilliseconds);

            Console.WriteLine();
            Console.WriteLine("test mit StringBuilder");
            StringBuilder str = new StringBuilder();
            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            for (int i = 0; i < 50000; i++)
                str = str.Append("x");
            watch2.Stop();
            Console.WriteLine("Zeit: {0}", watch2.ElapsedMilliseconds);

            Console.WriteLine();
            Console.WriteLine("test mit umgewandeltem StringBuilder");
            str = new StringBuilder();
            Stopwatch watch3 = new Stopwatch();
            watch3.Start();
            for (int i = 0; i < 50000; i++)
                str.Append("x").ToString();
            watch3.Stop();
            Console.WriteLine("Zeit: {0}", watch3.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
