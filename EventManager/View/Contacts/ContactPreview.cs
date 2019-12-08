using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventManager.Model;
using EventManager.Utility;

namespace EventManager.View.Contacts
{
    public partial class ContactPreview : UserControl
    {
        CommonUtil commonUtil = new CommonUtil();
        public ContactPreview()
        {
            InitializeComponent();
        }

        public Contact ContactDetails
        {
            set{
                lbl_name.Text = value.Name;
                this.lbl_name.Left = this.Width / 2 - this.lbl_name.Width / 2;
                lbl_email.Text = value.Email;
                lbl_phone.Text = value.Phone;
                lbl_addressline1.Text = $"{value.AddressLine1} {value.AddressLine2}";
                lbl_addressline2.Text = $"{value.City} {value.State}  {value.Zipcode}";
                cpb_userimage.Image = commonUtil.Base64ToBitmap(value.Image);
                cpb_userimage.Left = this.Width / 2 - cpb_userimage.Width / 2;
            }
        }

    }
}
