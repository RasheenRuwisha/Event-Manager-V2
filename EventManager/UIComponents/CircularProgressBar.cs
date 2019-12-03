using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.UIComponents
{
    public partial class CircularProgressBar : UserControl
    {
        int progress;
        string text;
        double mutiplier;
       public int Progress
        {
            set
            {
                progress = value;
            }
        }


        public string Text
        {
            set
            {
                text = value;
            }
        }

        public double Multiplier
        {
            set
            {
                mutiplier = value;
            }
        }

        public CircularProgressBar()
        {
            progress = 10;
            InitializeComponent();
        }

        private void CircularProgressBar_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2);
            e.Graphics.RotateTransform(-90);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            Pen pen = new Pen(Color.Red);
            Rectangle rectangle = new Rectangle(0 - this.Width / 2+20, 0 - this.Height / 2+20, this.Width-40, this.Height-40);
            e.Graphics.DrawPie(pen, rectangle, 0, (int)(this.progress * mutiplier));
            e.Graphics.FillPie(new SolidBrush(Color.FromArgb(24, 174, 191)), rectangle, 0, (int)(this.progress * mutiplier));

            pen = new Pen(Color.White);
            rectangle = new Rectangle(0 - this.Width / 2 + 30, 0 - this.Height / 2 + 30, this.Width - 60, this.Height - 60);
            e.Graphics.DrawPie(pen, rectangle, 0, 360);
            e.Graphics.FillPie(new SolidBrush(Color.FromArgb(25, 25, 25)), rectangle, 0, 360);

            Font font = new Font("Arial", 12);
            e.Graphics.RotateTransform(90);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(text, font, new SolidBrush(Color.White), rectangle,stringFormat);

        }

        public void UpdateProgress(int progress)
        {
            this.progress = progress;
            this.Invalidate();
        }
    }
}
