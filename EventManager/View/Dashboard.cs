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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.View
{
    public partial class Dashboard : Form
    {
        readonly CommonUtil commonUtil = new CommonUtil();
        readonly PredictionUtility predictionUtility = new PredictionUtility();
        string ActivePanel = "Event";
        public Dashboard()
        {
            InitializeComponent();
            if (Application.UserAppDataRegistry.GetValue("username") != null)
            {
                this.lbl_username.Text = Application.UserAppDataRegistry.GetValue("username").ToString();
            }


            pnl_events.BringToFront();
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

            this.dtp_searchstart.ValueChanged += new EventHandler(endDate_ValueChanged);
            this.dtp_seachend.ValueChanged += new EventHandler(endDate_ValueChanged);

            dtp_searchstart.Value = DateTime.Today;
            dtp_seachend.Value = DateTime.Today.AddHours(24).AddSeconds(-1);
            cpb_userimage.Image = commonUtil.Base64ToBitmap(Application.UserAppDataRegistry.GetValue("image").ToString());
        }


        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_seachend.Value < dtp_searchstart.Value)
            {
                dtp_seachend.Value = dtp_searchstart.Value.AddHours(24).AddSeconds(-1); ;
            }
        }



        // Contact Manegement Start

        private void cpb_addcontact_Click(object sender, EventArgs e)
        {
            AddContact addContact = new AddContact();
            addContact.FormClosing += new FormClosingEventHandler(this.AddContact_FormClosing);
            addContact.ShowDialog();
        }

        /// <summary>
        /// Calls the GenerateContactList Function and allow iterates through the ContactListViews that were returned, and then add the controls to the view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void pnl_contactlist_Paint(object sender, PaintEventArgs e)
        {
            if(pnl_contactlist.Controls.Count == 0) { 
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
                pnl_contactlist.Controls.Add(this.GenerateNotFoundLabel("No Contact(s) Found"));
            }
            }
            pnl_contactlist.BringToFront();
            txt_search.Enabled = true;
            pb_search.Enabled = true;
        }


        /// <summary>
        /// Requires the type of the contactList to be generated
        /// Once generetaed add the retails of the contacts to the contact list and returns ContactListView to be added to the controls
        /// </summary>
        /// <param name="type">The type wether its "load" or "search" and depending on the type the releavant panel is been loaded</param>
        /// <returns>The List of Contacts that should be added to the panels</returns>
        private List<ContactListView> GenerateContactList(String type)
        {
            this.AddSeachHeaderAndClose();

            List<Contact> contactList = new List<Contact>();
            if (type.Equals("load"))
            {
                contactList = ContactHelper.GetUserContacts();
            }
            else if (type.Equals("search"))
            {
                contactList = ContactHelper.GetUserContactsByName(txt_search.Text.Trim());
            }
            List<ContactListView> contactLists = new List<ContactListView>();
            foreach (Contact contactDetails in contactList)
            {
                ContactListView contact = new ContactListView
                {
                    Tag = contactDetails.ContactId,
                    ContactName = contactDetails.Name,
                    ContactEmail = contactDetails.Email,
                    ContactId = contactDetails.ContactId,
                    ContactImage = commonUtil.Base64ToBitmap(contactDetails.Image),
                    Name = $"ctx_"
                };
                contact.Click += new EventHandler(this.ContactControlClick);
                contactLists.Add(contact);

            }
            return contactLists;
        }

        /// <summary>
        /// Adds the search header and close button, within an async task,
        /// If Invoking is required the method is then reinvoked.
        /// </summary>
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


        /// <summary>
        /// Adds the search results to the Search COntacts Panel and, and if no contacts are available a message is shown to the user.
        /// </summary>
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
                pnl_search.Controls.Add(this.GenerateNotFoundLabel("No Contact(s) Found"));
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
            Contact contact = ContactHelper.GetContactDetails(contactList.Tag.ToString());
            ContactPreview contactPreview = new ContactPreview
            {
                ContactName = contact.Name,
                ContactEmail = contact.Email,
                ContactAddressLine1 = contact.AddressLine1 + ", " + contact.AddressLine2,
                ContactAddressLine2 = contact.City + ", " + contact.State + ", " + contact.Zipcode,
                ContactImage = commonUtil.Base64ToBitmap(contact.Image)
            };

            this.pnl_contactpreview.Controls.Clear();
            this.pnl_contactpreview.Controls.Add(contactPreview);
        }


        /// <summary>
        /// Generates the No Contact Label to be added to the event panel or the contacts panel
        /// </summary>
        /// <param name="text">The text that should be in the label</param>
        /// <returns>The label with the not found text.</returns>
        private Label GenerateNotFoundLabel(string text)
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


        private void pb_event_close_Click(object sender, EventArgs e)
        {
            pnl_eventlist.BringToFront();
        }



        // Contact Manegement End


        // Event Management Start

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


        private List<EventListView> GenerateEventList(String type)
        {
            WeekStartEnd weekStartEnd = new WeekStartEnd
            {
                WeekStart = DateTime.Now.Date,
                WeekEnd = DateTime.Now.AddDays(7).AddSeconds(-1).Date

        };

            this.AddEventSeachHeaderAndClose();

            List<UserEvent> contactList = new List<UserEvent>();
            if (type.Equals("load"))
            {
                contactList = EventHelper.SearchUserEvent(weekStartEnd.WeekStart,weekStartEnd.WeekEnd);
            }
            else if (type.Equals("search"))
            {
                contactList = EventHelper.SearchUserEvent(dtp_searchstart.Value.Date, dtp_seachend.Value.Date.AddHours(24).AddSeconds(-1));
            }
            List<EventListView> contactLists = new List<EventListView>();
            foreach (UserEvent contactDetails in contactList)
            {
                EventListView contact = new EventListView
                {
                    userEvent = contactDetails,
                };
                contact.Click += new EventHandler(this.EventControlClick);
                //contact.deleteEvent = new EventHandler(this.removePreview);
                //contact.editEvent = new FormClosingEventHandler(this.generateContactPreview);
                contactLists.Add(contact);

            }
            return contactLists;
        }

        public void EventControlClick(object sender, EventArgs e)
        {
            EventListView eventListView = (EventListView)sender;
            UserEvent contact = eventListView.Tag as UserEvent;
            EventPreview contactPreview = new EventPreview
            {
                userEvent = contact
            };
            this.pnl_eventspreview.Controls.Clear();
            this.pnl_eventspreview.Controls.Add(contactPreview);
        }

        private async void AddEventSearchResults()
        {
            this.pnl_eventsearch.Controls.Clear();
            List<EventListView> contacts = await Task.Run(() => this.GenerateEventList("search"));
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
                pnl_eventsearch.Controls.Add(this.GenerateNotFoundLabel("No Event(s) Found"));
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
                List<EventListView> contacts = await Task.Run(() => this.GenerateEventList("load"));
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
                    pnl_eventlist.Controls.Add(this.GenerateNotFoundLabel("No Event(s) Found"));
                }
            }
            
            pnl_eventlist.BringToFront();
            txt_search.Enabled = true;
            pb_search.Enabled = true;
        }

        // Event Management End


        // Prediction Start


   
        private async void pnl_predictiondet_Paint(object sender, PaintEventArgs e)
        {
            Prediction prediction = new Prediction();
            prediction = await Task.Run(() => predictionUtility.PredictTimeConsumption());

            if(prediction.EventCount > 0)
            {
                cpbar_tasks.Progress = prediction.TaskCount;
                cpbar_tasks.Multiplier = 360 / prediction.EventCount;
                cpbar_tasks.CircleText = $"{prediction.TaskCount} / {prediction.EventCount}";

                cpbar_appointments.Progress = prediction.AppointmentCount;
                cpbar_appointments.Multiplier = 360 / prediction.EventCount;
                cpbar_appointments.CircleText = $"{prediction.AppointmentCount} / {prediction.EventCount}";

                lbl_dailyavgtext.Text = $"For the next month you might spend {Math.Round(prediction.DailyAverage / 60, 1)} hours  on average for Events Daily";
                lbl_weeklyavgtext.Text = $"For the next month you might spend {Math.Round(prediction.WeeklyAverage / 60, 1)} hours on average for Events Weekly";
                lbl_monthlyavgtext.Text = $"For the next month you might spend {Math.Round(prediction.MonthlyAverage / 60, 1)} hours on average for Events Monthly";

            }

            pnl_predictiondet.BringToFront();
        }

        // Prediction End


        // Common Controls Start

        private void btn_predictions_Click(object sender, EventArgs e)
        {
            if (!ActivePanel.Equals("Prediction"))
            {
                this.pnl_prediction.BringToFront();
                this.pnl_predloader.BringToFront();
                ActivePanel = "Prediction";
                this.btn_events.ForeColor = Color.White;
                this.btn_contact.ForeColor = Color.White;
                this.btn_predictions.ForeColor = Color.FromArgb(24, 174, 191);
            }

        }

        private void cpb_refresh_Click(object sender, EventArgs e)
        {
            this.pnl_eventloader.BringToFront();
            this.pnl_eventlist.Controls.Clear();
            this.pnl_eventlist.Refresh();
        }


        private void btn_events_Click(object sender, EventArgs e)
        {
            if (!ActivePanel.Equals("Event"))
            {
                pnl_eventloader.BringToFront();
                pnl_events.BringToFront();
                ActivePanel = "Event";
                this.btn_contact.ForeColor = Color.White;
                this.btn_predictions.ForeColor = Color.White;
                this.btn_events.ForeColor = Color.FromArgb(24, 174, 191);
            }
        }

        private void btn_contact_Click(object sender, EventArgs e)
        {
            if (!ActivePanel.Equals("Contact"))
            {
                pnl_contacts.BringToFront();
                pnl_loader.BringToFront();
                ActivePanel = "Contact";
                this.btn_events.ForeColor = Color.White;
                this.btn_predictions.ForeColor = Color.White;
                this.btn_contact.ForeColor = Color.FromArgb(24, 174, 191);
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            commonUtil.removeSavedData();
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void cpb_pred_addevent_Click(object sender, EventArgs e)
        {
            AddEvent addEvent = new AddEvent();
            addEvent.FormClosing += new FormClosingEventHandler(this.AddEvent_FormClosing);
            addEvent.ShowDialog();
        }

        private void cpb_pred_adduser_Click(object sender, EventArgs e)
        {
            AddContact addContact = new AddContact();
            addContact.FormClosing += new FormClosingEventHandler(this.AddContact_FormClosing);
            addContact.ShowDialog();
        }

        private void cpb_pred_refresh_Click(object sender, EventArgs e)
        {
            this.pnl_predloader.BringToFront();
            this.pnl_predictiondet.Controls.Clear();
            this.pnl_predictiondet.Refresh();
        }

        private void cpb_cont_addevent_Click(object sender, EventArgs e)
        {
            AddEvent addEvent = new AddEvent();
            addEvent.FormClosing += new FormClosingEventHandler(this.AddEvent_FormClosing);
            addEvent.ShowDialog();
        }

        private void cpb_cont_adduser_Click(object sender, EventArgs e)
        {
            AddContact addContact = new AddContact();
            addContact.FormClosing += new FormClosingEventHandler(this.AddContact_FormClosing);
            addContact.ShowDialog();
        }

        private void cpb_cont_refresh_Click(object sender, EventArgs e)
        {
            this.pnl_loader.BringToFront();
            this.pnl_contactlist.Controls.Clear();
            this.pnl_contactlist.Refresh();
        }

        private void cpb_evnt_addcont_Click(object sender, EventArgs e)
        {
            AddContact addContact = new AddContact();
            addContact.FormClosing += new FormClosingEventHandler(this.AddContact_FormClosing);
            addContact.ShowDialog();
        }

        private void btn_profile_Click(object sender, EventArgs e)
        {
        }

        private void cpb_userimage_Click(object sender, EventArgs e)
        {
            UserProfile userProfile = new UserProfile();
            userProfile.ShowDialog();
        }



        // Common Controls End


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if(Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
            base.OnFormClosing(e);
        }
    }

}
