using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lab_03_WPF
{
    public partial class MainWindow : Window
    {
        List<Rectangle> strips = new List<Rectangle>();
        Random rnd = new Random();
        DispatcherTimer timer = new DispatcherTimer();

        int N = 5;
        int appeared = 0;
        bool gameOver = false;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int w = 150, h = 30;
            if (rnd.Next(2) == 0)
            {
                w = 30; h = 150;
            }

            int x = rnd.Next(0, (int)canvas1.ActualWidth - w);
            int y = rnd.Next(0, (int)canvas1.ActualHeight - h);

            Rectangle r = new Rectangle();
            r.Width = w;
            r.Height = h;
            r.Fill = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256)));
            r.Stroke = Brushes.Black;

            Canvas.SetLeft(r, x);
            Canvas.SetTop(r, y);

            r.MouseDown += Rect_MouseDown;

            canvas1.Children.Add(r);
            strips.Add(r);
            appeared++;

            this.Title = "Полоски. Появилось: " + appeared + ", на экране: " + strips.Count;

            if (strips.Count >= 2 * N)
            {
                timer.Stop();
                gameOver = true;
                if (strips.Count > 0)
                {
                    MessageBox.Show("Вы проиграли!");
                }
            }
        }

        private void Rect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (gameOver) return;
            if (appeared < N) return;

            Rectangle r = (Rectangle)sender;
            int index = strips.IndexOf(r);

            Rect myRect = new Rect(Canvas.GetLeft(r), Canvas.GetTop(r), r.Width, r.Height);

            bool covered = false;
            for (int j = index + 1; j < strips.Count; j++)
            {
                Rectangle other = strips[j];
                Rect otherRect = new Rect(Canvas.GetLeft(other), Canvas.GetTop(other), other.Width, other.Height);
                if (myRect.IntersectsWith(otherRect))
                {
                    covered = true;
                }
            }

            if (covered == false)
            {
                canvas1.Children.Remove(r);
                strips.Remove(r);
            }

            if (strips.Count == 0)
            {
                timer.Stop();
                gameOver = true;
                MessageBox.Show("Вы выиграли!");
            }
        }
    }
}
