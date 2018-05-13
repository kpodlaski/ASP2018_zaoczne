using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulki
{
    public class World
    {
        public List<Ball> balls = new List<Ball>();
        public readonly float width;
        public readonly float heigth;

        public World(int NumberOfBalls, float w, float width, float height)
        {
            this.width = width;
            this.heigth = height;
            for (int i=0; i<NumberOfBalls ; i++)
            {
                balls.Add(new Ball(w, this));
            }
        }

        internal void Start()
        {
            foreach(Ball b in balls)
            {
                b.Start();
            }
        }
    }
}
