using System;
using System.Threading;

namespace Mutexy {
    public class Officer {
        public static int max_id = 1;
        public static Object counterMonitor = new object();
        public static Random rand = new Random();
        public int Id { get; private set; }
        public volatile bool IsFree;

        public Officer() {
            lock (counterMonitor) {
                Id = max_id++;
            }
            IsFree = true;
        }

        public void service(Custommer c) {
            IsFree = false;
            Console.WriteLine("Urzędnik "+Id+" Obsługuje klienta " + c.Name);
            Thread.Sleep(rand.Next() % 300 + 1000);
            Console.WriteLine("Klient obsłużony " + c.Name);
            IsFree = true;
        }
    }
}