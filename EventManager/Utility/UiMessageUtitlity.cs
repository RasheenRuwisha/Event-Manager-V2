using EventManager.UIComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.Utility
{
    public class UiMessageUtitlity
    {

        public PictureBox AddErrorIcon(string name, int x, int y)
        {
            PictureBox picture = new PictureBox();
            picture.Location = new Point(x, y);
            picture.Name = "ptx_" + name;
            picture.Size = new Size(25, 25);
            picture.BackColor = Color.Transparent;
            picture.Image = Properties.Resources.erroricon;
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            return picture;
        }

        public PictureBox AddPasswordErrorIcon(string name, int x, int y)
        {
            PictureBox picture = new PictureBox();
            picture.Location = new Point(x, y);
            picture.Name = "ptx_" + name;
            picture.Size = new Size(25, 35);
            picture.BackColor = Color.Transparent;
            picture.Image = Properties.Resources.passworderror;
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            return picture;
        }

        public Banner AddBanner(string message, string type)
        {
            Banner banner = new Banner();
            banner.Dock = DockStyle.Top;
            banner.BannerDescription = message;

            if (type.Equals("error"))
            {
                banner.BackColor = Color.FromArgb(248, 215, 218);
                banner.BannerForeGround = Color.FromArgb(114, 28, 36);
                banner.BannerBackGround = Color.FromArgb(248, 215, 218);
                banner.BannerTitle = "Error";
            }
            else if (type.Equals("success"))
            {
                banner.BackColor = Color.FromArgb(212, 237, 218);
                banner.BannerForeGround = Color.FromArgb(21, 87, 36);
                banner.BannerBackGround = Color.FromArgb(212, 237, 218);
                banner.BannerTitle = "Success";
            }
            return banner;
        }

    }
}
