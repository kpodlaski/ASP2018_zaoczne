using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuleWatkow {
    public partial class Okno : Form {
        public Okno() {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e) {
            Job job = new Job();
            Task<int> oczekujeNaLiczbe = job.AccessTheWebAsync();
            this.label1.Text = "Czekam .....";
            new Task(() => {
                Console.WriteLine("Czekam");
                //label1.Text = "Dalej czekam ....";
                updateOperation uOp = new updateOperation(UpdateLabel);
                BeginInvoke(uOp);
            }).Start();
            
            int liczba = await oczekujeNaLiczbe;
            this.label1.Text = " liczba " + liczba;
        }

        public delegate void updateOperation ();

        void UpdateLabel() {
            label1.Text = "Dalej czekam ....";
        }
        
    }
}
