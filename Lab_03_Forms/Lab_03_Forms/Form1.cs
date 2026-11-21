using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_03_Forms
{
    public class Strip
    {
        public Rectangle Rect;
        public Color Color;
    }

    public partial class Form1 : Form
    {
        List<Strip> strips = new List<Strip>();
        Random rnd = new Random();
        Timer timer1 = new Timer();

        int N = 5;
        int appeared = 0;

        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;
            Paint += Form1_Paint;
            MouseDown += Form1_MouseDown;

            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int w = 150;
            int h = 30;
            if (rnd.Next(2) == 1)
            {
                w = 30;
                h = 150;
            }

            Strip s = new Strip();
            s.Rect = new Rectangle(rnd.Next(ClientSize.Width - w), rnd.Next(ClientSize.Height - h), w, h);
            s.Color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

            strips.Add(s);
            appeared++;

            Text = "Появилось: " + appeared + ", на экране: " + strips.Count;
            Refresh();

            if (strips.Count >= 2 * N)
            {
                timer1.Stop();
                MessageBox.Show("Вы проиграли!");
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Strip s in strips)
            {
                e.Graphics.FillRectangle(new SolidBrush(s.Color), s.Rect);
                e.Graphics.DrawRectangle(Pens.Black, s.Rect);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (timer1.Enabled == false) return;
            if (appeared < N) return;

            for (int i = strips.Count - 1; i >= 0; i--)
            {
                if (strips[i].Rect.Contains(e.Location))
                {
                    bool covered = false;
                    for (int j = i + 1; j < strips.Count; j++)
                    {
                        if (strips[j].Rect.IntersectsWith(strips[i].Rect))
                            covered = true;
                    }

                    if (covered == false)
                    {
                        strips.RemoveAt(i);
                        Text = "Появилось: " + appeared + ", на экране: " + strips.Count;
                        Refresh();

                        if (strips.Count == 0)
                        {
                            timer1.Stop();
                            MessageBox.Show("Вы выиграли!");
                        }
                    }
                    break;
                }
            }
        }
    }
}
