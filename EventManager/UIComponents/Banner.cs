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
    public partial class Banner : UserControl
    {
        public Banner()
        {
            InitializeComponent();
        }

        public String BannerTitle
        {
            set
            {
                lbl_title.Text = value;
            }
        }

        public String BannerDescription
        {
            set
            {
                lbl_txt.Text = value;
            }
        }

        public Color BannerForeGround
        {
            set
            {
                this.lbl_title.ForeColor = value;
                this.lbl_txt.ForeColor = value;
            }
        }

        public Color BannerBackGround
        {
            set
            {
                this.lbl_title.BackColor = value;
                this.lbl_txt.BackColor = value;
            }
        }

        private void pb_close_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }
    }
}
