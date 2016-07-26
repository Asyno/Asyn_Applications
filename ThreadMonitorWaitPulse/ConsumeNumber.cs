using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadMonitorWaitPulse
{
    class ConsumeNumber
    {
        private MyNumber obj;

        public ConsumeNumber(MyNumber obj)
        {
            this.obj = obj;
        }

        public void GetNumber()
        {
            Monitor.Enter(obj);

            // wenn sich der Erzeugerthread im Wartezustand
            // befindet, ihn 'bereit' schalten
            if (Program.thread1Waiting)
                Monitor.Pulse(obj);

            Program.thread2Waiting = true;

            while (Monitor.Wait(obj))
            {
                Console.WriteLine("Nummer {0} verbraucht", obj.Number);
                Monitor.Pulse(obj);
                if (Program.finished) Thread.CurrentThread.Abort();
            }
            Monitor.Exit(obj);
        }
    }
}
