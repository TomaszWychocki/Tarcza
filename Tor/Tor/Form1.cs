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
            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = DrawArea;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {
            Graphics g;
            g = Graphics.FromImage(DrawArea);
            g.Clear(Color.White);
            Pen mypen = new Pen(Brushes.Black);
            g.DrawLine(mypen, 0, 220, 440, 220);
            g.DrawLine(mypen, 220, 0, 220, 440);
            pictureBox1.Image = DrawArea;
            g.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g;
            g = Graphics.FromImage(DrawArea);
            Pen mypen = new Pen(Brushes.Black);
            reset();

            float r = Convert.ToSingle(textBox1.Text) * metr;
            float omega = Convert.ToSingle(textBox2.Text);
            float v = Convert.ToSingle(textBox3.Text) * metr;
            bool two = false;
            g.DrawEllipse(mypen, 220 - (r / 2), 220 - (r / 2), r, r);
            //-------------------------------------------------------
            double kat = 90;

            do
            {
                SetPixel(
                    g, 
                    Convert.ToInt32((r / 2 * Math.Cos(kat * Math.PI / 180)) + 220), 
                    Convert.ToInt32((r / 2 * Math.Sin(kat * Math.PI / 180)) + 220)
                    );

                if (!two)
                    r -= v;
                else
                    r += v;

                kat += omega;
                if (kat > 360)
                    kat = 0;

                if (r <= 0)
                {
                    two = true;
                    r = 0;
                }

            }
            while (r != Convert.ToInt32(textBox1.Text) * metr);

            pictureBox1.Image = DrawArea;
            g.Dispose();
        }

        private void SetPixel(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.Black, new Rectangle(x, y, 1, 1));
        }
    }
}
