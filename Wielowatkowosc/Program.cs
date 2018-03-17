using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wielowatkowosc {
    class Program {
        static void Main(string[] args) {
            //Watki();
            WyścigiKonne();
        }

        private static void WyścigiKonne() {
            Kon[] konie = new Kon[5];
            Random r = new Random();
            Barrier start = new Barrier(konie.Length, (x)=> { Console.WriteLine("START"); });
            Kon.start = start;
            Barrier finish = new Barrier(konie.Length, (x) => { Console.WriteLine("FINISH"); });
            Kon.finish = finish;
            int l = 1000;
            for(int k = 0; k < konie.Length; k++) {
                konie[k] = new Kon();
                konie[k].v = r.NextDouble() * 20;
                konie[k].distance = l;
                konie[k].watek = new Thread(konie[k].run);
                konie[k].nrStartowy = k;
                /*if (k == 3)
                    konie[k].watek.Priority = ThreadPriority.Highest;
                else
                    konie[k].watek.Priority = ThreadPriority.Lowest;
                */
                konie[k].watek.Start();
            }
            Console.ReadLine();
        }

        static void Watki() {
            Barrier barrier = new Barrier(1001,
                (x) => { Console.WriteLine("Bariera Zwolniona"); });
            for (int i = 0; i < 1000; i++) {
                Zadanie z = new Wielowatkowosc.Zadanie();
                z.NrZadania=i;
                z.Barrier = barrier;
                Thread t = new Thread(z.WykonajZadanie);
                t.Start();
            }
            Thread t2 = new Thread(()=> {
                Console.WriteLine("Text1");
                Console.WriteLine("Text2");
                Console.WriteLine("Text3");
            });
            t2.Start();
            barrier.SignalAndWait();
            //t2.Join();
            //foreach (Thread th in t) th.Join();
            
            Console.WriteLine("Zadania skończone");
            Console.WriteLine(Zadanie.TotalValue);
            Console.ReadLine();
        }
    }

    class Kon {
        internal double v;
        internal int distance;
        private double x = 0.0;
        internal Thread watek;
        internal static Barrier start;
        internal int nrStartowy;
        internal static Barrier finish;

        internal void run() {
            start.SignalAndWait();
            while (x < distance) {
                x += v;
                Console.WriteLine("["+nrStartowy+"] "+ x +" m");
            }
            int ms = DateTime.Now.Millisecond;
            finish.SignalAndWait();
            Console.WriteLine("Dobiegłem [" + nrStartowy + "] czas = "+ms);
        }
    }

    class Zadanie {
        public static volatile int TotalValue = 0;
        public static String monitor = "MONITOR";
        public int NrZadania;

        public Barrier Barrier { get; internal set; }

        public void WykonajZadanie() {
            for (int i=0; i<10; i++) {
                //Console.WriteLine("["+NrZadania+"]  i:" + i);
                lock(monitor) {
                    TotalValue += i;
                }
            }
            Console.WriteLine("[" + NrZadania + "]  Skończone");
            Barrier.SignalAndWait();
        }
    }


}
