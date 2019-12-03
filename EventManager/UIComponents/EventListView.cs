using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventManager.DatabaseHelper;
using EventManager.Model;
using EventManager.View.Events;

namespace EventManager.UIComponents
{
    public partial class EventListView : UserControl
    {
        public EventListView()
        {
            InitializeComponent();
        }

        public UserEvent userEvent
        {
            set
            {
                this.lbl_title.Text = value.Title;
                this.lbl_startdate.Text = value.StartDate.ToString();
                this.lbl_enddate.Text = value.EndDate.ToString();
                this.Tag = value;

                this.lbl_enddate.Location = new Point(this.lbl_startdate.Width + 20, this.lbl_startdate.Location.Y);

            }
        }

        private void pb_delete_Click(object sender, EventArgs e)
        {

            EventHelper eventHelper = new EventHelper();
            UserEvent userEvent = this.Tag as UserEvent;
          
                var confirmResult = MessageBox.Show("Are you sure to delete this event?",
                                "Confirm Delete!!",
                                MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {


                    eventHelper.RemoveEvent(userEvent.EventId);
                    Panel panel = this.Parent as Panel;

                    panel.Controls.Clear();
                    panel.Refresh();
                }
                else
                {
                }

        }

        private void pb_edit_Click(object sender, EventArgs e)
        {
            UserEvent userEvent = this.Tag as UserEvent;
            EditEvent editEvent = new EditEvent(userEvent.EventId);
            editEvent.ShowDialog();

            Panel panel = this.Parent as Panel;

            panel.Controls.Clear();
            panel.Refresh();
        }
    }
}
