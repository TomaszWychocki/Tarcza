using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tor
{
    public partial class Form1 : Form
    {
        Bitmap DrawArea;
        float metr = 30;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reset();
            Draw();
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

        private void drawLines()
        {
            Graphics g;
            g = Graphics.FromImage(DrawArea);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen mypen = new Pen(Brushes.Black);
            Pen arrow = new Pen(Brushes.Black, 8);
            g.DrawLine(mypen, 0, pictureBox1.Size.Height / 2, pictureBox1.Size.Width, pictureBox1.Size.Height / 2);
            g.DrawLine(mypen, pictureBox1.Size.Width / 2, 0, pictureBox1.Size.Width / 2, pictureBox1.Size.Height);

            float r = Convert.ToSingle(trackBar1.Value) * metr;
            r += 40;

            if (Convert.ToSingle(trackBar2.Value) > 0)
            {
                arrow.StartCap = LineCap.ArrowAnchor;
                g.DrawArc(
                    arrow,
                    (pictureBox1.Size.Width / 2) - (r / 2), 
                    (pictureBox1.Size.Height / 2) - (r / 2),
                    Convert.ToInt32(r),
                    Convert.ToInt32(r), 
                    220, 
                    20);
            }
            else if(Convert.ToSingle(trackBar2.Value) != 0)
            {
                arrow.EndCap = LineCap.ArrowAnchor;
                g.DrawArc(
                    arrow,
                    (pictureBox1.Size.Width / 2) - (r / 2),
                    (pictureBox1.Size.Height / 2) - (r / 2),
                    Convert.ToInt32(r),
                    Convert.ToInt32(r),
                    220,
                    20);
            }

            float f = 0;
            for(int i = pictureBox1.Size.Width/2; i <= pictureBox1.Size.Width; i+=Convert.ToInt32(metr))
            {
                g.DrawLine(mypen, i, (pictureBox1.Size.Height / 2) - 4, i, (pictureBox1.Size.Height / 2) + 4);
                if(f!=0)
                    g.DrawString(f.ToString(), new Font("calibri", 10, FontStyle.Regular), Brushes.Black, i-5, (pictureBox1.Size.Height / 2) + 8);
                f++;
            }
            f = 0;
            for (int i = pictureBox1.Size.Width / 2; i >= 0; i -= Convert.ToInt32(metr))
            {
                g.DrawLine(mypen, i, (pictureBox1.Size.Height / 2) - 4, i, (pictureBox1.Size.Height / 2) + 4);
                if (f != 0)
                    g.DrawString(f.ToString(), new Font("calibri", 10, FontStyle.Regular), Brushes.Black, i - 9, (pictureBox1.Size.Height / 2) + 8);
                f--;
            }

            f = 0;
            for (int i = pictureBox1.Size.Height / 2; i <= pictureBox1.Size.Height; i += Convert.ToInt32(metr))
            {
                g.DrawLine(mypen, (pictureBox1.Size.Width / 2) - 4, i, (pictureBox1.Size.Width / 2) + 4, i);
                if (f != 0)
                    g.DrawString(f.ToString(), new Font("calibri", 10, FontStyle.Regular), Brushes.Black, (pictureBox1.Size.Width / 2) + 8, i-9);
                f--;
            }
            f = 0;
            for (int i = pictureBox1.Size.Height / 2; i >= 0; i -= Convert.ToInt32(metr))
            {
                g.DrawLine(mypen, (pictureBox1.Size.Width / 2) - 4, i, (pictureBox1.Size.Width / 2) + 4, i);
                if (f != 0)
                    g.DrawString(f.ToString(), new Font("calibri", 10, FontStyle.Regular), Brushes.Black, (pictureBox1.Size.Width / 2) + 8, i - 9);
                f++;
            }

            pictureBox1.Image = DrawArea;
            g.Dispose();
        }

        private void Draw()
        {
            Graphics g;
            g = Graphics.FromImage(DrawArea);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen BluePen = new Pen(Brushes.Blue, 2);
            reset();

            float r = Convert.ToSingle(trackBar1.Value) * metr;
            float omega = Convert.ToSingle(trackBar2.Value);
            float v = Convert.ToSingle(trackBar3.Value) * metr;
            List<Point> pointList = new List<Point>();
            bool two = false;
            g.FillEllipse(new SolidBrush(Color.LightGray), (pictureBox1.Size.Width / 2) - (r / 2), (pictureBox1.Size.Height / 2) - (r / 2), r, r);
            //-------------------------------------------------------
            double kat = 90;
            pointList.Add(new Point(
                Convert.ToInt32((r / 2 * Math.Cos(kat * Math.PI / 180)) + (pictureBox1.Size.Width / 2)), 
                Convert.ToInt32((r / 2 * Math.Sin(kat * Math.PI / 180)) + (pictureBox1.Size.Height / 2))
                ));

            do
            {
                if (!two)
                    r -= v/5;
                else
                    r += v/5;

                kat += omega/5;
                if (kat == 360)
                    kat = 0;

                if (r <= 0)
                {
                    two = true;
                    r = 0;
                }

                if (r > Convert.ToSingle(trackBar1.Value) * metr)
                    r = Convert.ToSingle(trackBar1.Value) * metr;

                pointList.Add(new Point(
                    Convert.ToInt32((r / 2 * Math.Cos(kat * Math.PI / 180)) + (pictureBox1.Size.Width / 2)),
                    Convert.ToInt32((r / 2 * Math.Sin(kat * Math.PI / 180)) + (pictureBox1.Size.Height / 2))
                    ));

                if (r == Convert.ToSingle(trackBar1.Value) * metr)
                    break;
            }
            while (r < Convert.ToInt32(trackBar1.Value) * metr);

            Point[] pointArray = pointList.ToArray();
            g.DrawCurve(BluePen, pointArray, 0.3f);

            drawLines();
            pictureBox1.Image = DrawArea;
            g.Dispose();
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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "Promień tarczy [m]: " + trackBar1.Value;
            Draw();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label2.Text = "Prękość kątowa [rad/s]: " + trackBar2.Value;
            Draw();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label3.Text = "Prędkość obiektu [m/s]: " + trackBar3.Value;
            Draw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "JPEG (*.jpg)|*.jpg";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
            }
        }
    }
}
