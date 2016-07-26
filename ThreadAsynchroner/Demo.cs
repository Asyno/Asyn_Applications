using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadAsynchroner
{
    class Demo
    {
        // asynchron aufzurufende Methode
        public string DoSomething(int x, ref long y)
        {
            // zeitintensive Ausführung
            for(int i = 0; i <= 30; i++)
            {
                Console.Write("X");
                Thread.Sleep(10);
            }
            y = 12345;
            return "Ich habe fertig.";
        }
    }
}
