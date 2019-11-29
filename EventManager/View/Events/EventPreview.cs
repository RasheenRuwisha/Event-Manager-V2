using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.View.Events
{
    public partial class EventPreview : UserControl
    {
        public EventPreview()
        {
            InitializeComponent();
        }

        public String title
        {
            set
            {
                lbl_title.Text = value;
            }
        }


        public String description
        {
            set
            {
                lbl_desscription.Text = value;
            }
        }

        public String startdate
        {
            get
            {
                return lbl_startdate.Text;
            }

            set
            {
                lbl_startdate.Text = value;
            }
        }

        public String enddate
        {
            get
            {
                return lbl_enddate.Text;
            }

            set
            {
                lbl_enddate.Text = value;
                this.lbl_enddate.Location = new Point(this.lbl_startdate.Width + 10, this.lbl_startdate.Location.Y);

            }
        }

        public String repeattype
        {
            set
            {
                lbl_repeattype.Text = value;
            }
        }

        public String addressline1
        {
            set
            {
                lbl_address1header.Visible = true;
                lbl_address1.Visible = true;
                lbl_address1.Text = value;
            }
        }

        public String addressline2
        {
            set
            {
                lbl_address2header.Visible = true;
                lbl_address2.Visible = true;
                lbl_address2.Text = value;

            }
        }
    }
}
