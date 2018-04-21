using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kulki
{
    public class Ball
    {
        public float X { private set; get;}
        public float Y { private set; get;}
        public float Vx { private set; get; }
        public float Vy { private set; get; }
        private Thread thread;
        static Random rand = new Random();

        public Ball()
        {
            X = rand.Next() % 500 + 100;
            Y = rand.Next() % 500 + 100;
            Vy = (float) (rand.NextDouble() * 10 - 5);
            Vx = (float)(rand.NextDouble() * 10 - 5);
            thread = new Thread(() =>
            {
                while (true)
                {
                    move();
                    Thread.Sleep(30);
                }
            });
        }

        public void move()
        {
            X += Vx;
            Y += Vy;
        }

        public void Start()
        {
            thread.Start();
        }

    }
}
