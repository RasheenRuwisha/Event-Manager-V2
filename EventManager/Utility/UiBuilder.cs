using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.Utility
{
    public class UiBuilder
    {

        public TextBox GenerateLongTextBox(int x, int y, String name, String text, int maxLength)
        {
            TextBox textBox = new TextBox();
            textBox.AutoSize = false;
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(250, 30);
            textBox.Name = name;
            textBox.MaxLength = maxLength;
            textBox.Text = text;
            textBox.BackColor = Color.FromArgb(31, 31, 31);
            textBox.ForeColor = Color.White;
            textBox.BorderStyle = BorderStyle.Fixed3D;
            textBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            return textBox;
        }


        public TextBox GenerateShortTextBox(int x, int y, String name, String text, int maxLength)
        {
            TextBox textBox = new TextBox();
            textBox.AutoSize = false;
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(128, 30);
            textBox.Name = name;
            textBox.Text = text;
            textBox.MaxLength = maxLength;
            textBox.BackColor = Color.FromArgb(31, 31, 31);
            textBox.ForeColor = Color.White;
            textBox.BorderStyle = BorderStyle.Fixed3D;
            textBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            return textBox;
        }

        public Label GenerateLabel(int x, int y, String name, String text)
        {
            Label label = new Label();
            label.Location = new Point(x, y);
            label.Name = name;
            label.Text = text;
            label.ForeColor = Color.White;
            return label;
        }

        public PictureBox GeneratePictureBox(int x, int y, String name, Image image, int height, int width)
        {
            PictureBox pBox = new PictureBox();
            pBox.Location = new Point(x, y);
            pBox.Size = new Size(width, height);
            pBox.SizeMode = PictureBoxSizeMode.Zoom;
            pBox.Name = name;
            pBox.Image = image;
            return pBox;
        }

    }
}
