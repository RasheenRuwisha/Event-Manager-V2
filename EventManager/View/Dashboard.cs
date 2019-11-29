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
                pnl_contactlist.Controls.Add(this.GenerateNoContacsLabel());
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
                contact.Tag = contactDetails.Contactid;
                contact.ContactName = contactDetails.Name;
                contact.ContactEmail = contactDetails.Email;
                contact.ContactId = contactDetails.Contactid;
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
                pnl_search.Controls.Add(this.GenerateNoContacsLabel());
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

        private Label GenerateNoContacsLabel()
        {
            Label label = new Label()
            {
                AutoSize = true,
                Font = new System.Drawing.Font("HoloLens MDL2 Assets", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.SystemColors.ControlLightLight,
                Name = "lbl_error",
                Size = new System.Drawing.Size(175, 29),
                TabIndex = 44,
                Text = "No Contact(s) Found",
            };
            label.Location = new System.Drawing.Point(pnl_search.Width / 2 - label.Width / 2, pnl_search.Height / 2 - label.Height / 2);
            return label;
        }

        private void btn_events_Click(object sender, EventArgs e)
        {
            pnl_events.BringToFront();
        }

        private void btn_contact_Click(object sender, EventArgs e)
        {
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
            this.AddSeachHeaderAndClose();

            List<UserEvent> contactList = new List<UserEvent>();
            if (type.Equals("load"))
            {
                contactList = eventHelper.GetUserEvents();
            }
            else if (type.Equals("search"))
            {
                contactList = eventHelper.SearchUserEvent(txt_eventsearch.Text.Trim());
            }
            List<EventListView> contactLists = new List<EventListView>();
            foreach (UserEvent contactDetails in contactList)
            {
                EventListView contact = new EventListView();
                contact.Tag = contactDetails.eventid;
                contact.EventTitle = contactDetails.title;
                contact.EventStartDate = contactDetails.StartDate.ToString();
                contact.EventEndDate = contactDetails.EndDate.ToString();
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
            UserEvent contact = this.eventHelper.GetUserEvent(contactList.Tag.ToString());
            EventPreview contactPreview = new EventPreview();
            contactPreview.title = contact.title;
            contactPreview.description = contact.description;
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
                pnl_eventsearch.Controls.Add(this.GenerateNoContacsLabel());
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
                pnl_contactlist.Controls.Add(this.GenerateNoContacsLabel());
            }
            pnl_eventlist.BringToFront();
            txt_search.Enabled = true;
            pb_search.Enabled = true;
        }
    }
}
