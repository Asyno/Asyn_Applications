using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPriority
{
    public class Demo
    {
        public void Execution1()
        {
            for (int i = 0; i <= 500; i++)
                Console.Write(".");
        }

        public void Execution2()
        {
            for (int number = 0; number <= 10; number++)
                Console.WriteLine("It's me, Thread2");
        }
    }
}
