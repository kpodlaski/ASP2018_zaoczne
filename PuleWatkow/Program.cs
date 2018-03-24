using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuleWatkow {
    class Program {

        public static void Main(string[] a) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Okno());
        }

        static void MainThreadPools(string[] args) {
            ThreadPool.SetMaxThreads(40,20);
            ThreadPool.SetMinThreads(20, 20);
            for (int i=0; i<50; i++) {
                Job job = new Job();
                job.id = i;
                ThreadPool.QueueUserWorkItem(job.taskToBeDone);
                
            }
            Console.ReadKey();
        }
    }

    class Job {
        public int id { get; set; }
       public void taskToBeDone(Object state) {
            for (int i = 0; i < 5; i++) {
                Console.WriteLine(id+" : "+ i);
                Thread.Sleep(100);
            }
        }

        public async Task<int> AccessTheWebAsync() { 
            HttpClient client = new HttpClient();
            Console.WriteLine("Starting Task");
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");
            Console.WriteLine("Task started");
            DoIndependentWork();
            string urlContents = await getStringTask;
            Console.WriteLine("End method");
            return urlContents.Length;
        }

        private void DoIndependentWork() {
            Console.WriteLine("Doing something independent");
        }
    }
}
