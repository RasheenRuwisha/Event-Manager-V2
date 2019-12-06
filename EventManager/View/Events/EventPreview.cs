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

namespace EventManager.View.Events
{
    public partial class EventPreview : UserControl
    {
        public EventPreview()
        {
            InitializeComponent();
        }

        public UserEvent userEvent
        {
            set
            {
                lbl_title.Text = value.Title;
                lbl_startdate.Text = value.StartDate.ToString();
                lbl_enddate.Text = $"- {value.EndDate.ToString()}";
                if (!value.AddressLine1.Equals(""))
                {
                    lbl_address1header.Visible = true;
                    lbl_address1.Visible = true;
                    lbl_address1.Text = value.AddressLine1;
                }
                if (!value.City.Equals("") || !value.State.Equals(""))
                {
                    lbl_address2header.Visible = true;
                    lbl_address2.Visible = true;
                    lbl_address2.Text = value.AddressLine2 + value.City + value.State + value.Zipcode;
                }
                lbl_repeattype.Text = value.RepeatType.Equals("") ? "None" : value.RepeatType;
                lbl_description.Text = value.Description;
                this.lbl_enddate.Location = new Point(this.lbl_startdate.Width + 20, this.lbl_startdate.Location.Y);

                if(value.EventContacts != null)
                {
                    if(value.EventContacts.Count != 0)
                    {
                        lbl_collabs.Visible = true;
                        int x = 18;
                        int y = 310;
                        foreach (EventContact eventContact in value.EventContacts)
                        {
                            Label label = new Label()
                            {
                                Text = eventContact.ContactName,
                                Name = $"lbl_{eventContact.ContactName}",
                                Location = new Point(x,y)
                            };
                            Controls.Add(label);
                            y += 30;
                        }
                    }
                }
            }
        }

        
    }
}
