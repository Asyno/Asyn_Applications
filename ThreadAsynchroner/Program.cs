using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadAsynchroner
{
    public delegate string MyDelegate(int x, ref long y);

    class Program
    {
        private static MyDelegate del;
        private static int intVar = 4711;
        private static long lngVar;

        static void Main(string[] args)
        {
            Demo obj = new Demo();
            del = new MyDelegate(obj.DoSomething);
            AsyncCallback callback = new AsyncCallback(CallbackMethod);

            // die Methode AsyncTest in Demo asynchron aufrufen
            // mit den Parametern intVar und lngVar, void währe möglich
            del.BeginInvoke(intVar, ref lngVar, callback, null);

            // zeitaufwendige Ausführung

            for(int i = 0; i <= 100; i++)
            {
                Console.Write(".");
                Thread.Sleep(10);
            }
            Console.ReadLine();
        }

        public static void CallbackMethod(IAsyncResult ar)
        {
            Console.Write(del.EndInvoke(ref lngVar, ar));
            Console.Write(".. Wert y = {0}", lngVar);
        }
    }
}
