using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kulki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        World world;
        public MainWindow()
        {
            float ball_width = 60;
            int N = 4;
            InitializeComponent();
            world = new World(N, ball_width, (float) MainCanvas.Width, (float) MainCanvas.Height);
            buildBalls(ball_width);
            
            updateScreen();
        }

        List<Ellipse> EBalls = new List<Ellipse>();
        Brush[] brushes = new Brush[] { Brushes.Azure, Brushes.Bisque, Brushes.BlueViolet, Brushes.Chocolate };
        private void buildBalls(float ball_width)
        {
            for (int i = 0; i < world.balls.Count; i++)
            {
                Ellipse eBall = new Ellipse();
                eBall.Fill = brushes[i % brushes.Length];
                eBall.Stroke = brushes[i % brushes.Length];
                eBall.Height = ball_width;
                eBall.Width = ball_width;
                MainCanvas.Children.Add(eBall);
                EBalls.Add(eBall);
            }
        }

        public void updateScreen()
        {
                for (int i = 0; i < world.balls.Count; i++)
                {
                    Canvas.SetTop(EBalls[i], world.balls[i].Y - EBalls[i].Height / 2);
                    Canvas.SetLeft(EBalls[i], world.balls[i].X - EBalls[i].Width / 2);
                }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer =
                new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            dispatcherTimer.Start();
            world.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            updateScreen();
            //ball1.move();
        }
    }
}
