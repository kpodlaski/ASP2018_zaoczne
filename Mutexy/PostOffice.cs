using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mutexy {
    class PostOffice {
        List<Officer> officer = new List<Officer>();
        Semaphore semaphore;

        public PostOffice(int numberOfOfficers) {
            for(int i=0; i<numberOfOfficers; i++) {
                officer.Add(new Officer());
                semaphore = new Semaphore(numberOfOfficers, numberOfOfficers, "PostOfice");
            }
        }

        public void AddToQueue(Custommer c) {
            semaphore.WaitOne();//czy możemy wejść;
            Officer actualOfficer =null;
            lock (officer) { //pewność że oficer nie będzie przydzielony dwum osobom
                foreach (Officer o in officer) {
                    if (o.IsFree) {
                        actualOfficer = o;
                        o.IsFree = false;
                        break;
                    }
                }
            }
            actualOfficer.service(c);
            semaphore.Release(); //zwalniamy miejsce w okienku
        }
    }
}
