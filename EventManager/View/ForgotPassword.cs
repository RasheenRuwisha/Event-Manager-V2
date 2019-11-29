using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.View
{
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();


            this.logo.Image = Properties.Resources.logo;
            this.logo.AutoSize = false;
            this.logo.Left = this.Width / 2 - this.logo.Width / 2;

            this.txt_email.AutoSize = false;
            this.txt_email.Size = new System.Drawing.Size(250, 30);

            this.txt_password.AutoSize = false;
            this.txt_password.Size = new System.Drawing.Size(250, 30);

            this.txt_confirmpasword.AutoSize = false;
            this.txt_confirmpasword.Size = new System.Drawing.Size(250, 30);

            this.txt_otp.AutoSize = false;
            this.txt_otp.Size = new System.Drawing.Size(250, 30);

            this.email_panel.BringToFront();
        }
    }
}
