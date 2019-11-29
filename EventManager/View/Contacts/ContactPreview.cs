using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.View.Contacts
{
    public partial class ContactPreview : UserControl
    {
        public ContactPreview()
        {
            InitializeComponent();
        }

        public String ContactName
        {
            set
            {
                lbl_name.Text = value;
                this.lbl_name.Left = this.Width / 2 - this.lbl_name.Width / 2;
            }
        }

        public String ContactEmail
        {
            set
            {
                lbl_email.Text = value;
            }
        }

        public String ContactAddressLine1
        {
            set
            {
                lbl_addressline1.Text = value;
            }
        }

        public String ContactAddressLine2
        {
            set
            {
                lbl_addressline2.Text = value;
            }
        }

        public Image ContactImage
        {
            set
            {
                cpb_userimage.Image = value;
                this.cpb_userimage.Left = this.Width / 2 - this.cpb_userimage.Width / 2;
            }
        }
    }
}
