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
using EventManager.Utility;

namespace EventManager.UIComponents
{
    public partial class EventListView : UserControl
    {

        readonly CommonUtil commonUtil = new CommonUtil();

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
                this.lbl_enddate.Text = $"- {value.EndDate.ToString()}";
                this.Tag = value;

                this.lbl_enddate.Location = new Point(this.lbl_startdate.Width + 20, this.lbl_startdate.Location.Y);

            }
        }

        private void pb_delete_Click(object sender, EventArgs e)
        {

            UserEvent userEvent = this.Tag as UserEvent;
          
            if(userEvent.RepeatType.Equals("") || userEvent.RepeatType.Equals("None"))
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this event?",
                               "Confirm Delete!!",
                               MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    
                    Panel panel = this.Parent as Panel;
                    Panel panelPreview = Parent.Parent.Controls.Find("pnl_eventspreview", true).FirstOrDefault() as Panel;

                    DeleteEvent deleteEvent = new DeleteEvent("event",userEvent.EventId, panel, panelPreview);
                    deleteEvent.ShowDialog();
                }
            }
            else
            {
                RepeatEventConfirmation repeatEventConfirmation = new RepeatEventConfirmation(userEvent);
                repeatEventConfirmation.ShowDialog();
                Panel panel = Parent.Parent.Controls.Find("pnl_eventlist", true).FirstOrDefault() as Panel;
                Panel panelPreview = Parent.Parent.Controls.Find("pnl_eventspreview", true).FirstOrDefault() as Panel;
                panelPreview.Controls.Clear();

                panel.Controls.Clear();
                panel.Refresh();
                panel.BringToFront();

                Panel parentPanel = Parent as Panel;
                if (parentPanel != null)
                {
                    parentPanel.Controls.Clear();

                }
            }
               

        }

        private void pb_edit_Click(object sender, EventArgs e)
        {
            UserEvent userEvent = this.Tag as UserEvent;
            EditEvent editEvent = new EditEvent(userEvent);
            editEvent.ShowDialog();
            Panel panel = Parent.Parent.Controls.Find("pnl_eventlist", true).FirstOrDefault() as Panel;
            Panel panelPreview = Parent.Parent.Controls.Find("pnl_eventspreview", true).FirstOrDefault() as Panel;
            panelPreview.Controls.Clear();

            panel.Controls.Clear();
            panel.Refresh();
            panel.BringToFront();

            Panel parentPanel = Parent as Panel;
            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();
            }
            }
        }
}
