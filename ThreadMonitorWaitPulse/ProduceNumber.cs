using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadMonitorWaitPulse
{
    class ProduceNumber
    {
        private MyNumber obj;

        public ProduceNumber(MyNumber obj)
        {
            this.obj = obj;
        }

        public void MakeNumber()
        {
            Random rnd = new Random();
            Monitor.Enter(obj);
            for(int i = 0; i <= 10; i++)
            {
                Program.thread1Waiting = true;

                // falls der Konsumthread noch nicht im Wartezustand ist,
                // selbst in den Wartezustand gehen
                if (Program.thread2Waiting == false)
                    Monitor.Wait(obj);
                obj.Number = rnd.Next(0, 1000);
                Console.WriteLine("Nummer {0} erzeugt", obj.Number);

                // dem nächsten in der Warteschlange stehenden Objekt
                // den Monitor übergeben
                Monitor.Pulse(obj);
                Program.thread2Waiting = false;
            }
            Program.finished = true;
            Monitor.Exit(obj);
        }
    }
}
