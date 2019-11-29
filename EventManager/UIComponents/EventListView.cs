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
    public partial class EventListView : UserControl
    {
        public EventListView()
        {
            InitializeComponent();
        }


        public String EventTitle
        {
            set
            {
                this.lbl_title.Text = value;
            }
        }

        public String EventStartDate
        {
            get
            {
                return lbl_startdate.Text;
            }
            set
            {
                this.lbl_startdate.Text = value;

            }
        }

        public String eventId
        {
            set
            {
                this.pb_delete.Tag = value;
            }
        }


        public String EventEndDate
        {

            get
            {
                return lbl_enddate.Text;
            }
            set
            {
                this.lbl_enddate.Text = value;
                this.lbl_enddate.Location = new Point(this.lbl_startdate.Width + 20, this.lbl_startdate.Location.Y);

            }
        }
    }
}
