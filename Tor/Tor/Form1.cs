using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tor
{
    public partial class Form1 : Form
    {
        Bitmap DrawArea;
        float metr = 10;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {
            Graphics g;
            g = Graphics.FromImage(DrawArea);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Gray);
            pictureBox1.Image = DrawArea;
            g.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            Graphics g;
            g = Graphics.FromImage(DrawArea);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen RedPen = new Pen(Brushes.Blue, 2);
            reset();

            float r = Convert.ToSingle(textBox1.Text) * metr;
            float omega = Convert.ToSingle(textBox2.Text);
            float v = Convert.ToSingle(textBox3.Text) * metr;
            Point[] curvePoints = new Point[10000];
            int i = 0;
            bool two = false;
            g.FillEllipse(new SolidBrush(Color.LightGray), (pictureBox1.Size.Width / 2) - (r / 2), (pictureBox1.Size.Height / 2) - (r / 2), r, r);
            //-------------------------------------------------------
            double kat = 90;
            curvePoints[i] = new Point(
                Convert.ToInt32((r / 2 * Math.Cos(kat * Math.PI / 180)) + (pictureBox1.Size.Width / 2)), 
                Convert.ToInt32((r / 2 * Math.Sin(kat * Math.PI / 180)) + (pictureBox1.Size.Height / 2))
                );
            i++;

            do
            {
                if (!two)
                    r -= v;
                else
                    r += v;

                kat += omega;
                if (kat == 360)
                    kat = 0;

                if (r <= 0)
                {
                    two = true;
                    r = 0;
                }

                curvePoints[i] = new Point(
                    Convert.ToInt32((r / 2 * Math.Cos(kat * Math.PI / 180)) + (pictureBox1.Size.Width / 2)),
                    Convert.ToInt32((r / 2 * Math.Sin(kat * Math.PI / 180)) + (pictureBox1.Size.Height / 2))
                    );
                i++;

            }
            while (r < Convert.ToInt32(textBox1.Text) * metr);

            g.DrawCurve(RedPen, curvePoints);
            Pen mypen = new Pen(Brushes.Black);
            g.DrawLine(mypen, 0, pictureBox1.Size.Height / 2, pictureBox1.Size.Width, pictureBox1.Size.Height / 2);
            g.DrawLine(mypen, pictureBox1.Size.Width / 2, 0, pictureBox1.Size.Width / 2, pictureBox1.Size.Height);
            pictureBox1.Image = DrawArea;
            g.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = Convert.ToString(Convert.ToDouble(textBox2.Text) + 0.1);
            Draw();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = Convert.ToString(Convert.ToDouble(textBox2.Text) - 0.1);
            Draw();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(Size.Width - 241, Size.Height - 24);
            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = DrawArea;
            groupBox1.Location = new Point(Size.Width - 223, 12);
            button4.Location = new Point(Size.Width - 87, Size.Height - 51);
            label4.Location = new Point(Size.Width - 138, Size.Height - 25);
        }
    }
}
