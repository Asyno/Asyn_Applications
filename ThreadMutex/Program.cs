using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ThreadMutex
{
    class Program
    {
        private static Mutex mutex;

        static void Main(string[] args)
        {
            if (IsApplicationStarted())
            {
                Console.WriteLine("Die Anwendung wurde bereits gestartet");
                Console.WriteLine("Ein Zweiter Start ist nicht möglich.");
            }
            else
            {
                Console.WriteLine("Die Anwendung wird gestartet.");
                Console.WriteLine("Die Anwendung läuft.");
            }
            Console.ReadLine();
        }

        public static bool IsApplicationStarted()
        {
            string mutexName = Application.ProductName;
            mutex = new Mutex(false, mutexName);
            if (mutex.WaitOne(0, true))
                return false;
            else
                return true;
        }
    }
}
