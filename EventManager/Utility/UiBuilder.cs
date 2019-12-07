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

        public TextBox GenerateLongTextBox(int x, int y, String name, String text, int maxLength, int tabindex)
        {
            TextBox textBox = new TextBox
            {
                AutoSize = false,
                Location = new Point(x, y),
                Size = new Size(250, 30),
                Name = name,
                MaxLength = maxLength,
                TabIndex = tabindex,
                Text = text,
                BackColor = Color.FromArgb(31, 31, 31),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Microsoft Sans Serif", 11.25F)
            };
            return textBox;
        }


        public TextBox GenerateShortTextBox(int x, int y, String name, String text, int maxLength , int tabindex)
        {
            TextBox textBox = new TextBox
            {
                AutoSize = false,
                Location = new Point(x, y),
                Size = new Size(128, 30),
                Name = name,
                Text = text,
                MaxLength = maxLength,
                BackColor = Color.FromArgb(31, 31, 31),
                TabIndex = tabindex,
                ForeColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Microsoft Sans Serif", 11.25F)
            };
            return textBox;
        }

        public Label GenerateLabel(int x, int y, String name, String text)
        {
            Label label = new Label
            {
                Location = new Point(x, y),
                Name = name,
                Text = text,
                ForeColor = Color.White
            };
            return label;
        }

        public PictureBox GeneratePictureBox(int x, int y, String name, Image image, int height, int width)
        {
            PictureBox pBox = new PictureBox
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                SizeMode = PictureBoxSizeMode.Zoom,
                Name = name,
                Image = image
            };
            return pBox;
        }

    }
}
