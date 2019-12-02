using EventManager.DatabaseHelper;
using EventManager.Model;
using EventManager.UIComponents;
using EventManager.Utility;
using EventManager.View.Contacts;
using EventManager.View.Events;
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
    public partial class Dashboard : Form
    {
        ContactHelper contactHelper = new ContactHelper();
        EventHelper eventHelper = new EventHelper();
        CommonUtil commonUtil = new CommonUtil();

        public Dashboard()
        {
            InitializeComponent();
            panel1.BringToFront();
            pnl_eventloader.BringToFront();

            txt_search.Enabled = false;
            pb_search.Enabled = false;

            pnl_eventlist.AutoScroll = false;
            pnl_eventlist.HorizontalScroll.Enabled = false;
            pnl_eventlist.HorizontalScroll.Visible = false;
            pnl_eventlist.HorizontalScroll.Maximum = 0;
            pnl_eventlist.AutoScroll = true;

            pnl_eventsearch.AutoScroll = false;
            pnl_eventsearch.HorizontalScroll.Enabled = false;
            pnl_eventsearch.HorizontalScroll.Visible = false;
            pnl_eventsearch.HorizontalScroll.Maximum = 0;
            pnl_eventsearch.AutoScroll = true;

            pnl_contactlist.AutoScroll = false;
            pnl_contactlist.HorizontalScroll.Enabled = false;
            pnl_contactlist.HorizontalScroll.Visible = false;
            pnl_contactlist.HorizontalScroll.Maximum = 0;
            pnl_contactlist.AutoScroll = true;

            pnl_search.AutoScroll = false;
            pnl_search.HorizontalScroll.Enabled = false;
            pnl_search.HorizontalScroll.Visible = false;
            pnl_search.HorizontalScroll.Maximum = 0;
            pnl_search.AutoScroll = true;

            this.dtp_searchstart.ValueChanged += new EventHandler(startDatePickerValueChanged);
        }

        void startDatePickerValueChanged(object sender, EventArgs e)
        {
            if (dtp_seachend.Value < dtp_searchstart.Value)
            {
                this.dtp_seachend.Value = this.dtp_searchstart.Value;

            }
        }

        private void cpb_addcontact_Click(object sender, EventArgs e)
        {
            AddContact addContact = new AddContact();
            addContact.FormClosing += new FormClosingEventHandler(this.AddContact_FormClosing);
            addContact.ShowDialog();
        }

        private async void pnl_contactlist_Paint(object sender, PaintEventArgs e)
        {
            List<ContactListView> contacts = await Task.Run(() => this.GenerateContactList("load"));
            if (contacts.Count != 0)
            {
                int x = 0;
                int y = 0;
                int count = 0;
                foreach (ContactListView contactList in contacts)
                {
                    if (count == 0)
                    {
                        contactList.Location = new Point(x, y);
                        this.pnl_contactlist.Controls.Add(contactList);
                        y = y + contactList.Height + 1;
                        //x = x + contactList.Width + 1;
                        count = 1;
                    }
                    else if (count == 1)
                    {
                        contactList.Location = new Point(x, y);
                        this.pnl_contactlist.Controls.Add(contactList);
                        y = y + contactList.Height + 1;
                        contactList.BackColor = Color.FromArgb(39, 39, 39);
                        x = 0;
                        count = 0;

                    }
                }
 
            }
            else
            {
                pnl_contactlist.Controls.Add(this.GenerateNoContacsLabel("No Contact(s) Found"));
            }
            pnl_contactlist.BringToFront();
            txt_search.Enabled = true;
            pb_search.Enabled = true;
        }

        private List<ContactListView> GenerateContactList(String type)
        {
            this.AddSeachHeaderAndClose();

            List<Contact> contactList = new List<Contact>();
            if (type.Equals("load"))
            {
                contactList = contactHelper.GetUserContacts();
            }
            else if (type.Equals("search"))
            {
                contactList = contactHelper.GetUserContactsByName(txt_search.Text.Trim());
            }
            List<ContactListView> contactLists = new List<ContactListView>();
            foreach (Contact contactDetails in contactList)
            {
                ContactListView contact = new ContactListView();
                contact.Tag = contactDetails.ContactId;
                contact.ContactName = contactDetails.Name;
                contact.ContactEmail = contactDetails.Email;
                contact.ContactId = contactDetails.ContactId;
                contact.ContactImage = commonUtil.Base64ToBitmap(contactDetails.Image);
                contact.Name = $"ctx_";
                contact.Click += new EventHandler(this.ContactControlClick);
                //contact.deleteEvent = new EventHandler(this.removePreview);
                //contact.editEvent = new FormClosingEventHandler(this.generateContactPreview);
                contactLists.Add(contact);

            }
            return contactLists;
        }


        private void AddSeachHeaderAndClose()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.AddSeachHeaderAndClose));
            }
            else
            {
                Label label = new Label()
                {
                    AutoSize = true,
                    Font = new System.Drawing.Font("HoloLens MDL2 Assets", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    ForeColor = System.Drawing.SystemColors.ControlLightLight,
                    Location = new System.Drawing.Point(218, 12),
                    Name = "lbl_header",
                    Size = new System.Drawing.Size(175, 29),
                    TabIndex = 44,
                    Text = "Search Results",
                    TextAlign = System.Drawing.ContentAlignment.TopCenter
                };


                PictureBox pictureBox = new PictureBox()
                {
                    Image = global::EventManager.Properties.Resources.whitex,
                    Location = new System.Drawing.Point(10, 11),
                    Name = "pb_close",
                    Size = new System.Drawing.Size(20, 20),
                    SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
                    TabIndex = 45,
                    TabStop = false,
                };
                pictureBox.Click += new System.EventHandler(this.pb_close_Click);
                pnl_search.Controls.Add(label);
                pnl_search.Controls.Add(pictureBox);
            }      
        }

        private void AddEventSeachHeaderAndClose()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.AddEventSeachHeaderAndClose));
            }
            else
            {
                Label label = new Label()
                {
                    AutoSize = true,
                    Font = new System.Drawing.Font("HoloLens MDL2 Assets", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    ForeColor = System.Drawing.SystemColors.ControlLightLight,
                    Location = new System.Drawing.Point(218, 12),
                    Name = "lbl_header",
                    Size = new System.Drawing.Size(175, 29),
                    TabIndex = 44,
                    Text = "Search Results",
                    TextAlign = System.Drawing.ContentAlignment.TopCenter
                };


                PictureBox pictureBox = new PictureBox()
                {
                    Image = global::EventManager.Properties.Resources.whitex,
                    Location = new System.Drawing.Point(10, 11),
                    Name = "pb_close",
                    Size = new System.Drawing.Size(20, 20),
                    SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
                    TabIndex = 45,
                    TabStop = false,
                };
                pictureBox.Click += new System.EventHandler(this.pb_event_close_Click);
                pnl_eventsearch.Controls.Add(label);
                pnl_eventsearch.Controls.Add(pictureBox);
            }
        }


        private void AddContact_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.pnl_contactlist.Controls.Clear();
            this.pnl_contactlist.Refresh();
            this.pnl_contactlist.BringToFront();
        }


        private async void AddSearchResults()
        {
            this.pnl_search.Controls.Clear();
            List<ContactListView> contacts = await Task.Run(() => this.GenerateContactList("search"));
            if (contacts.Count != 0)
            {
                int x = 0;
                int y = 60;
                int count = 0;
                foreach (ContactListView contactList in contacts)
                {
                    if (count == 0)
                    {
                        contactList.Location = new Point(x, y);
                        this.pnl_search.Controls.Add(contactList);
                        y = y + contactList.Height + 1;
                        //x = x + contactList.Width + 1;
                        count = 1;
                    }
                    else if (count == 1)
                    {
                        contactList.Location = new Point(x, y);
                        this.pnl_search.Controls.Add(contactList);
                        y = y + contactList.Height + 1;
                        contactList.BackColor = Color.FromArgb(39, 39, 39);
                        x = 0;
                        count = 0;

                    }
                }
                
            }
            else
            {
                pnl_search.Controls.Add(this.GenerateNoContacsLabel("No Contact(s) Found"));
            }
            pnl_search.BringToFront();
        }



        private void pb_search_Click(object sender, EventArgs e)
        {
            pnl_loader.BringToFront();
            AddSearchResults();
        }

        private void pb_close_Click(object sender, EventArgs e)
        {
            pnl_contactlist.BringToFront();
        }

        private void pb_event_close_Click(object sender, EventArgs e)
        {
            pnl_eventlist.BringToFront();
        }

        public void ContactControlClick(object sender, EventArgs e)
        {
            ContactListView contactList = (ContactListView)sender;
            Contact contact = this.contactHelper.GetContactDetails(contactList.Tag.ToString());
            ContactPreview contactPreview = new ContactPreview();
            contactPreview.ContactName = contact.Name;
            contactPreview.ContactEmail = contact.Email;
            contactPreview.ContactAddressLine1 = contact.AddressLine1 + ", " + contact.AddressLine2;
            contactPreview.ContactAddressLine2 = contact.City + ", " + contact.State + ", " + contact.Zipcode;
            contactPreview.ContactImage = commonUtil.Base64ToBitmap(contact.Image);

            this.pnl_contactpreview.Controls.Clear();
            this.pnl_contactpreview.Controls.Add(contactPreview);
        }

        private Label GenerateNoContacsLabel(string text)
        {
            Label label = new Label()
            {
                AutoSize = true,
                Font = new System.Drawing.Font("HoloLens MDL2 Assets", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.SystemColors.ControlLightLight,
                Name = "lbl_error",
                Size = new System.Drawing.Size(175, 29),
                TabIndex = 44,
                Text = text,
            };
            label.Location = new System.Drawing.Point(pnl_search.Width / 2 - label.Width / 2, pnl_search.Height / 2 - label.Height / 2);
            return label;
        }

        private void btn_events_Click(object sender, EventArgs e)
        {
            pnl_loader.BringToFront();
            pnl_contactlist.Controls.Clear();
            pnl_events.BringToFront();
        }

        private void btn_contact_Click(object sender, EventArgs e)
        {
            pnl_eventlist.Controls.Clear();
            pnl_contacts.BringToFront();
            pnl_loader.BringToFront();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            commonUtil.removeSavedData();
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void cpb_addevent_Click(object sender, EventArgs e)
        {
            AddEvent addEvent = new AddEvent();
            addEvent.FormClosing += new FormClosingEventHandler(this.AddEvent_FormClosing);
            addEvent.ShowDialog();
        }

        private void AddEvent_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.pnl_eventlist.Controls.Clear();
            this.pnl_eventlist.Refresh();
            this.pnl_eventlist.BringToFront();
        }



        private List<EventListView> GenerateEventtList(String type)
        {
            this.AddEventSeachHeaderAndClose();

            List<UserEvent> contactList = new List<UserEvent>();
            if (type.Equals("load"))
            {
                contactList = eventHelper.GetUserEvents();
            }
            else if (type.Equals("search"))
            {
                contactList = eventHelper.SearchUserEvent(dtp_searchstart.Value, dtp_seachend.Value);
            }
            List<EventListView> contactLists = new List<EventListView>();
            foreach (UserEvent contactDetails in contactList)
            {
                EventListView contact = new EventListView();
                contact.userEvent = contactDetails;
                contact.Name = $"ctx_";
                contact.Click += new EventHandler(this.EventControlClick);
                //contact.deleteEvent = new EventHandler(this.removePreview);
                //contact.editEvent = new FormClosingEventHandler(this.generateContactPreview);
                contactLists.Add(contact);

            }
            return contactLists;
        }

        public void EventControlClick(object sender, EventArgs e)
        {
            EventListView contactList = (EventListView)sender;
            UserEvent contact = contactList.Tag as UserEvent;
            EventPreview contactPreview = new EventPreview();
            contactPreview.title = contact.Title;
            contactPreview.description = contact.Description;
            contactPreview.startdate = contact.RepeatType;
            contactPreview.enddate = contact.RepeatTill.ToString();

            this.pnl_eventspreview.Controls.Clear();
            this.pnl_eventspreview.Controls.Add(contactPreview);
        }

        private async void AddEventSearchResults()
        {
            this.pnl_eventsearch.Controls.Clear();
            List<EventListView> contacts = await Task.Run(() => this.GenerateEventtList("search"));
            if (contacts.Count != 0)
            {
                int x = 0;
                int y = 60;
                int count = 0;
                foreach (EventListView contactList in contacts)
                {
                    if (count == 0)
                    {
                        contactList.Location = new Point(x, y);
                        this.pnl_eventsearch.Controls.Add(contactList);
                        y = y + contactList.Height + 1;
                        //x = x + contactList.Width + 1;
                        count = 1;
                    }
                    else if (count == 1)
                    {
                        contactList.Location = new Point(x, y);
                        this.pnl_eventsearch.Controls.Add(contactList);
                        y = y + contactList.Height + 1;
                        contactList.BackColor = Color.FromArgb(39, 39, 39);
                        x = 0;
                        count = 0;

                    }
                }

            }
            else
            {
                pnl_eventsearch.Controls.Add(this.GenerateNoContacsLabel("No Event(s) Found"));
            }
            pnl_eventsearch.BringToFront();
        }

        private void pb_eventsearch_Click(object sender, EventArgs e)
        {
            pnl_eventloader.BringToFront();
            AddEventSearchResults();
        }

        private async void pnl_eventlist_Paint(object sender, PaintEventArgs e)
        {
            if(pnl_eventlist.Controls.Count == 0)
            {
                List<EventListView> contacts = await Task.Run(() => this.GenerateEventtList("load"));
                if (contacts.Count != 0)
                {
                    int x = 0;
                    int y = 0;
                    int count = 0;
                    foreach (EventListView contactList in contacts)
                    {
                        if (count == 0)
                        {
                            contactList.Location = new Point(x, y);
                            this.pnl_eventlist.Controls.Add(contactList);
                            y = y + contactList.Height + 1;
                            //x = x + contactList.Width + 1;
                            count = 1;
                        }
                        else if (count == 1)
                        {
                            contactList.Location = new Point(x, y);
                            this.pnl_eventlist.Controls.Add(contactList);
                            y = y + contactList.Height + 1;
                            contactList.BackColor = Color.FromArgb(39, 39, 39);
                            x = 0;
                            count = 0;

                        }
                    }

                }
                else
                {
                    pnl_contactlist.Controls.Add(this.GenerateNoContacsLabel("No Event(s) Found"));
                }
            }
            
            pnl_eventlist.BringToFront();
            txt_search.Enabled = true;
            pb_search.Enabled = true;
        }

        private void cpb_refresh_Click(object sender, EventArgs e)
        {
            this.pnl_loader.BringToFront();
            this.pnl_eventlist.Controls.Clear();
            this.pnl_eventlist.Refresh();
        }
    }

   
}
