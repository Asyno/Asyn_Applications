using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadBeginEndInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            Mathematics math = new Mathematics();
            int value = 23;
            AsyncCallback callback = new AsyncCallback(CallbackMethod);

            // Aufruf der asnychronen Ausführung
            math.BeginCalculate(value, callback, math);
            for(int i = 0; i <= 100; i++)
            {
                Console.Write(".{0}.", i);
                Thread.Sleep(5);
            }
            Console.ReadLine();
        }

        public static void CallbackMethod(IAsyncResult ar)
        {
            Mathematics math = (Mathematics)ar.AsyncState;

            // Das Ergebnis der asynchronen Operation anholen
            int result = math.EndCalculate(ar);
            Console.WriteLine("---Result = {0}---", result);
            Console.WriteLine("---FERTIG---");
        }
    }
}
