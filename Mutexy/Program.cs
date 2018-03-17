using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mutexy {
    class Program {
        static void Main(string[] args) {
            int numberOfOfficers = 3;
            int numberOfClients = 6;

            PostOffice postOffice = new PostOffice(numberOfOfficers);
            for(int c=0; c<numberOfClients; c++) {
                Custommer cust = new Custommer() { Name = "" + (char)(c + 65) };
                new Thread(() => {
                    Console.WriteLine("Klient " + cust.Name + " przyszedł na pocztę");
                    postOffice.AddToQueue(cust);
                }).Start();
            }

            Console.ReadLine();
            
        }
    }
}
