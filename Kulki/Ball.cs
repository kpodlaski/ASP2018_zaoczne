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
        private volatile float _x;
        private volatile float _y;
        private float _r2;
        private World world;

        public float X { private set { _x = value; } get { return _x; } }
        public float Y { private set { _y = value; } get { return _y; } }

        public float R2 { private set { _r2 = value; } get { return _r2; } }

        public float W { private set;  get; }
        public float Vx { private set; get; }
        public float Vy { private set; get; }
        private Thread thread;
        static Random rand = new Random();

        public Ball(float w, World world)
        {
            this.world = world;    
            W = w;
            R2 = W * W;
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
            if (X-W/2 < 0 || X+W/2 > world.width) Vx *= -1;
            if (Y-W/2 < 0 || Y+W/2 > world.heigth) Vy *= -1;
            foreach (Ball b in world.balls)
            {
                if (this != b)
                {
                    this.ColidesWith(b);
                }
            }
        }

        public bool ColidesWith(Ball b)
        {
            double dx = (_x - b._x);
            double dy = (_y - b._y);
            double d = dx * dx + dy * dy;
            if (d <= _r2)
            {
                if (Vx * dx <=0) Vx *= -1;
                if (Vy * dy <= 0) Vy *= -1;
                return true;
            }
            return false;
        }

        public void Start()
        {
            thread.Start();
        }

    }
}
