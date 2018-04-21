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
        public Ball ball1 = new Ball();
        public MainWindow()
        {
            InitializeComponent();
            updateScreen();
        }

        public void updateScreen()
        {
            Canvas.SetTop(EBall1, ball1.Y);
            Canvas.SetLeft(EBall1, ball1.X);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer =
                new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
            ball1.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            updateScreen();
            //ball1.move();
        }
    }
}
