using EventManager.DatabaseHelper;
using EventManager.Model;
using EventManager.UIComponents;
using EventManager.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.View.Events
{
    public partial class EditEvent : Form
    {

        UiBuilder uiBuilder = new UiBuilder();
        UiMessageUtitlity uiMessage = new UiMessageUtitlity();
        ContactHelper contactHelper = new ContactHelper();
        EventHelper eventHelper = new EventHelper();
        Bitmap bitmap = new Bitmap(Properties.Resources.user);
        FieldValidator fieldValidator = new FieldValidator();
        CommonUtil commonUtil = new CommonUtil();
        List<Contact> contacts = new List<Contact>();
        List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();
        readonly String userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
        UserEvent userEvent = new UserEvent();
        public EditEvent()
        {
            InitializeComponent();
        }

        public EditEvent(UserEvent userEvent)
        {
            InitializeComponent();
            this.userEvent = userEvent;
            txt_name.Text = userEvent.title;
            txt_email.Text = userEvent.description;
            dtp_startdate.Value = userEvent.StartDate;
            dtp_starttime.Value = userEvent.StartDate;
            dtp_enddate.Value = userEvent.StartDate;
            dtp_endtime.Value = userEvent.EndDate;

            List<Contact> contacts = new List<Contact>();
            if(userEvent.EventContacts != null)
            {
                foreach(EventContact eventContact in userEvent.EventContacts)
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem()
                    {
                        Id  = eventContact.Id,
                        ContactId = eventContact.ContactId,
                        Name = eventContact.Contact.Name,
                    };
                    contacts.Add(eventContact.Contact);
                    cmb_evetncollab.DisplayMember = "Name";
                    cmb_evetncollab.ValueMember = "ContactId";
                    cmb_evetncollab.DisplayMember = "Name";
                    cmb_evetncollab.ValueMember = "ContactId";
                    cmb_evetncollab.Items.Add(comboBoxItem);
                    comboBoxItems.Add(comboBoxItem);

                }
            }

            List<Contact> allContacts = new List<Contact>();
            allContacts = contactHelper.GetUserContacts();
            allContacts.RemoveAll(x => contacts.Exists(y => y.Contactid == x.Contactid));


            if (allContacts != null)
            {
                foreach (Contact eventContact in allContacts)
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem()
                    {
                        ContactId = eventContact.Contactid,
                        Name = eventContact.Name,
                    };

                    cmb_contacts.DisplayMember = "Name";
                    cmb_contacts.ValueMember = "ContactId";
                    cmb_contacts.DisplayMember = "Name";
                    cmb_contacts.ValueMember = "ContactId";
                    cmb_contacts.Items.Add(comboBoxItem);
                }
                cmb_contacts.Items.Remove("Loading....");
                cmb_contacts.SelectedIndex = 0;

            }

            if (userEvent.type.Equals("Appointment"))
            {
                rb_appointment.Checked = true;
                this.AddAddressControls();
                this.changeConrolLocations();
            }

            cmb_repeattype.SelectedItem = userEvent.RepeatType;

           

        }


































        private void rb_appointment_CheckedChanged(object sender, EventArgs e)
        {

            if (rb_appointment.Checked)
            {
                this.AddAddressControls();
                this.changeConrolLocations();
            }
            else
            {
                this.RemoveDynamicUis();
                this.changeConrolLocations();
            }

        }


        private void changeConrolLocations()
        {
            Label lbl_repeatduration = Controls.Find("lbl_repeatduration", true).FirstOrDefault() as Label;
            ComboBox cmb_repeatfor = Controls.Find("cmb_repeatfor", true).FirstOrDefault() as ComboBox;
            TextBox txt_duration = Controls.Find("txt_duration", true).FirstOrDefault() as TextBox;
            DateTimePicker dtp_duration = Controls.Find("dtp_duration", true).FirstOrDefault() as DateTimePicker;
            Label lbl_repeatfor = Controls.Find("lbl_repeatfor", true).FirstOrDefault() as Label;

            if (rb_appointment.Checked)
            {
                lbl_collaborators.Location = new Point(41, 512);
                cmb_contacts.Location = new Point(41, lbl_collaborators.Location.Y + lbl_collaborators.Height + 10);
                lbl_addcollab.Location = new Point(299, cmb_contacts.Location.Y);
                cmb_evetncollab.Location = new Point(333, cmb_contacts.Location.Y);
                btn_removecollab.Location = new Point(589, cmb_contacts.Location.Y);
                lbl_repeat.Location = new Point(41, cmb_contacts.Location.Y + cmb_contacts.Height + 10);
                cmb_repeattype.Location = new Point(41, lbl_repeat.Location.Y + lbl_repeat.Height + 10);

                if (lbl_repeatfor != null)
                {
                    lbl_repeatfor.Location = new Point(41, this.cmb_repeattype.Location.Y + 30);
                    cmb_repeatfor.Location = new Point(41, lbl_repeatfor.Location.Y + 22);
                    btn_save.Location = new Point(41, cmb_repeatfor.Location.Y + cmb_repeatfor.Height + 10);
                }
                else
                {
                    btn_save.Location = new Point(41, cmb_repeattype.Location.Y + cmb_repeattype.Height + 10);
                }

                if (lbl_repeatduration != null)
                {
                    lbl_repeatduration.Location = new Point(333, this.cmb_repeattype.Location.Y + 30);
                    if (txt_duration != null)
                    {
                        txt_duration.Location = new Point(333, cmb_repeatfor.Location.Y);
                    }
                    else if(dtp_duration != null)
                    {
                        dtp_duration.Location = new Point(333, cmb_repeatfor.Location.Y);
                    }

                }





            }
            else
            {
                lbl_collaborators.Location = new Point(41, 388);
                cmb_contacts.Location = new Point(41, lbl_collaborators.Location.Y + lbl_collaborators.Height + 10);
                lbl_addcollab.Location = new Point(299, cmb_contacts.Location.Y);
                cmb_evetncollab.Location = new Point(333, cmb_contacts.Location.Y);
                btn_removecollab.Location = new Point(589, cmb_contacts.Location.Y);
                lbl_repeat.Location = new Point(41, cmb_contacts.Location.Y + cmb_contacts.Height + 10);
                cmb_repeattype.Location = new Point(41, lbl_repeat.Location.Y + lbl_repeat.Height + 10);

                if (lbl_repeatfor != null)
                {
                    lbl_repeatfor.Location = new Point(41, this.cmb_repeattype.Location.Y + 30);
                    cmb_repeatfor.Location = new Point(41, lbl_repeatfor.Location.Y + 22);
                    btn_save.Location = new Point(41, cmb_repeatfor.Location.Y + cmb_repeatfor.Height + 10);
                }
                else
                {
                    btn_save.Location = new Point(41, cmb_repeattype.Location.Y + cmb_repeattype.Height + 10);

                }
            }
        }


        private void AddAddressControls()
        {
            Label rLabel = Controls.Find("lbl_repeatfor", true).FirstOrDefault() as Label;
            if (rLabel != null)
            {
                this.Size = new Size(627, 750);
            }
            else
            {
                this.Size = new Size(627, 700);
            }

            PictureBox tbx = this.Controls.Find("dynamicpbx_chevdown", true).FirstOrDefault() as PictureBox;
            if (tbx != null)
            {
                tbx.Dispose();
            }
            this.Controls.Add(uiBuilder.GenerateLongTextBox(42, 400, "dynamictxt_addressline1", userEvent.AddressLine1));
            this.Controls.Add(uiBuilder.GenerateLongTextBox(330, 400, "dynamictxt_addressline2", userEvent.AddressLine2));
            this.Controls.Add(uiBuilder.GenerateShortTextBox(42, 467, "dynamictxt_city", userEvent.City ));
            this.Controls.Add(uiBuilder.GenerateShortTextBox(243, 467, "dynamictxt_state", userEvent.State ));
            this.Controls.Add(uiBuilder.GenerateShortTextBox(451, 467, "dynamictxt_zip", userEvent.Zipcode));
            this.Controls.Add(uiBuilder.GenerateLabel(40, 388, "dynamiclbl_addressline1", "Address Line 1 "));
            this.Controls.Add(uiBuilder.GenerateLabel(329, 388, "dynamiclbl_addressline2", "Address Line 2 "));
            this.Controls.Add(uiBuilder.GenerateLabel(40, 445, "dynamiclbl_city", "City "));
            this.Controls.Add(uiBuilder.GenerateLabel(241, 445, "dynamiclbl_state", "State "));
            this.Controls.Add(uiBuilder.GenerateLabel(449, 445, "dynamiclbl_zip", "Zip "));
            this.CenterToParent();
        }

        private String GetDynamicTextBoxValues(String name)
        {
            TextBox tbx = this.Controls.Find(name, true).FirstOrDefault() as TextBox;
            if (tbx != null)
            {
                return tbx.Text.Trim();
            }
            return "";
        }

        private bool RemoveDynamicUis()
        {
            Label rLabel = Controls.Find("lbl_repeatfor", true).FirstOrDefault() as Label;
            if (rLabel != null)
            {
                this.Size = new Size(627, 600);
            }
            else
            {
                this.Size = new Size(627, 550);
            }
            List<Control> controlsList = new List<Control>();
            foreach (Control currentControl in this.Controls)
            {

                if ((currentControl).Name.StartsWith("dynamic"))
                {
                    controlsList.Add(currentControl);
                }

            }

            foreach (Control control in controlsList)
            {
                control.Dispose();
            }
            this.CenterToParent();
            return true;
        }


        private void pb_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void AddEvent_Load(object sender, EventArgs e)
        {
            bool addContact = await Task.Run(() => this.doAddContacts());
        }

        private bool doAddContacts()
        {
            try
            {
                contacts = contactHelper.GetUserContacts();
                this.AddContactList();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void AddContactList()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.AddContactList));
            }
            else
            {
                if (contacts != null)
                {
                    foreach (Contact contact in contacts)
                    {
                        ComboBoxItem comboBoxItem = new ComboBoxItem()
                        {
                            ContactId = contact.Contactid,
                            Name = contact.Name
                        };
                        cmb_contacts.DisplayMember = "Name";
                        cmb_contacts.ValueMember = "ContactId";
                        cmb_evetncollab.DisplayMember = "Name";
                        cmb_evetncollab.ValueMember = "ContactId";
                        cmb_contacts.Items.Add(comboBoxItem);
                    }
                    cmb_contacts.Items.Remove("Loading....");

                }
            }

        }

        private void lbl_addcollab_Click(object sender, EventArgs e)
        {
            if (cmb_contacts.Text.Equals("") && cmb_evetncollab.Text.Equals("No More Contacts"))
            {
                MessageBox.Show("Please Select an contact!");
            }
            else
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem = cmb_contacts.SelectedItem as ComboBoxItem;
                comboBoxItems.Add(comboBoxItem);
                cmb_evetncollab.Items.Add(comboBoxItem);
                cmb_contacts.Items.Remove(comboBoxItem);
                btn_removecollab.Enabled = true;
                cmb_evetncollab.Items.Remove("No Contacts Added");

                if (cmb_contacts.Items.Count == 0)
                {
                    cmb_contacts.Items.Add("No More Contacts");
                    lbl_addcollab.Enabled = false;
                }
                else
                {
                    lbl_addcollab.Enabled = true;
                }

            }
        }

        private void btn_removecollab_Click(object sender, EventArgs e)
        {
            if (cmb_evetncollab.Text.Equals("") && cmb_evetncollab.Text.Equals("No Contacts Added"))
            {
                MessageBox.Show("Please Select an contact!");
            }
            else
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem = cmb_evetncollab.SelectedItem as ComboBoxItem;
                cmb_contacts.Items.Add(comboBoxItem);
                cmb_evetncollab.Items.Remove(comboBoxItem);
                comboBoxItems.Remove(comboBoxItem);
                lbl_addcollab.Enabled = true;
                cmb_contacts.Items.Remove("No More Contacts");

                if (cmb_evetncollab.Items.Count == 0)
                {
                    cmb_evetncollab.Items.Add("No Contacts Added");
                    btn_removecollab.Enabled = false;
                }
                else
                {
                    btn_removecollab.Enabled = true;
                }
            }
        }


        private EventDates generateStartEnd(DateTime startDateTime, DateTime endDateTime)
        {
            EventDates eventDate = new EventDates();
            eventDate.StartDate = startDateTime;
            eventDate.EndDate = endDateTime;
            return eventDate;

        }



        private void ShowErrors()
        {
            foreach (Control contorl in this.Controls)
            {
                if (contorl is TextBox)
                {
                    if (String.IsNullOrEmpty(contorl.Text.Trim()))
                    {
                        PictureBox pictureBox = Controls.Find("ptx_" + contorl.Name, true).FirstOrDefault() as PictureBox;
                        if (pictureBox == null)
                        {
                            PictureBox error = uiMessage.AddErrorIcon(contorl.Name, contorl.Location.X + 255, contorl.Location.Y + 2);
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new MethodInvoker(this.ShowErrors));
                            }
                            else
                            {
                                this.Controls.Add(error);
                            }

                        }
                    }
                    else
                    {
                        PictureBox pictureBox = Controls.Find("ptx_" + contorl.Name, true).FirstOrDefault() as PictureBox;
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new MethodInvoker(this.ShowErrors));
                        }
                        else
                        {
                            Controls.Remove(pictureBox);
                        }
                    }

                }

            }
        }

        private bool ValidateFields()
        {
            this.ShowErrors();
            if (txt_email.Text.Trim().Equals("") || txt_name.Text.Trim().Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool DoValidations()
        {
            return this.ValidateFields();
        }

        private List<EventContact> GenerateEventContacts()
        {
            List<EventContact> eventContacts = new List<EventContact>();
            foreach (ComboBoxItem cmb in comboBoxItems)
            {
                EventContact eventContact = new EventContact()
                {
                    ContactId = cmb.ContactId,
                    EventId = userEvent.eventid,
                    UserId = userId,
                };

                eventContacts.Add(eventContact);
            }
            return eventContacts;
        }

        DateTime GenerateEndTime()
        {
            ComboBox cComboBox = Controls.Find("cmb_repeatfor", true).FirstOrDefault() as ComboBox;
            TextBox durationText = Controls.Find("txt_duration", true).FirstOrDefault() as TextBox;

            DateTime endDate = new DateTime();
            if (cComboBox != null)
            {
                if (cComboBox.Text.Equals("Specific Number Of Times"))
                {
                    if (cmb_repeattype.Text.Equals("Daily"))
                    {
                        endDate = dtp_enddate.Value.Date.AddDays(Int32.Parse(durationText.Text)).AddSeconds(-1);

                    }
                    else if (cmb_repeattype.Text.Equals("Weekly"))
                    {
                        endDate = dtp_enddate.Value.Date.AddDays(Int32.Parse(durationText.Text) * 7).AddSeconds(-1);

                    }
                    else if (cmb_repeattype.Text.Equals("Monthly"))
                    {
                        endDate = dtp_enddate.Value.Date.AddMonths(Int32.Parse(durationText.Text)).AddSeconds(-1);

                    }
                }
                else if (cComboBox.Text.Equals("Forever"))
                {
                    endDate = DateTime.MaxValue;
                }
            }
            else
            {
                endDate = dtp_enddate.Value.Date;
            }
            return endDate;
        }
        private UserEvent GenerateUserEvent()
        {
            ComboBox cComboBox = Controls.Find("cmb_repeatfor", true).FirstOrDefault() as ComboBox;
            TextBox durationText = Controls.Find("txt_duration", true).FirstOrDefault() as TextBox;
            DateTimePicker durationTime = Controls.Find("dtp_duration", true).FirstOrDefault() as DateTimePicker;

            UserEvent events = new UserEvent()
            {
                AddressLine1 = this.GetDynamicTextBoxValues("dynamictxt_addressline1"),
                AddressLine2 = this.GetDynamicTextBoxValues("dynamictxt_addressline2"),
                State = this.GetDynamicTextBoxValues("dynamictxt_state"),
                City = this.GetDynamicTextBoxValues("dynamictxt_city"),
                Zipcode = this.GetDynamicTextBoxValues("dynamictxt_zip"),
                eventid = userEvent.eventid,
                userid = Application.UserAppDataRegistry.GetValue("userID").ToString(),
                title = txt_name.Text,
                description = txt_email.Text,
                RepeatType = cmb_repeattype.Text,
                EventContacts = GenerateEventContacts(),
                RepeatDuration = cComboBox != null ? cComboBox.Text : "",
                RepeatCount = durationText != null ? Int32.Parse(durationText.Text) : 0,
                RepeatTill = durationTime != null ? durationTime.Value : this.GenerateEndTime(),
                StartDate = dtp_startdate.Value.Date + dtp_starttime.Value.TimeOfDay,
                EndDate = dtp_enddate.Value.Date + dtp_endtime.Value.TimeOfDay,
            };
            if (rb_appointment.Checked)
            {
                events.type = "Appointment";
            }
            else
            {
                events.type = "Task";
            }

            return events;
        }


        private async void btn_save_Click(object sender, EventArgs e)
        {
            bool isAppointment = false;
            Tasks tasks = new Tasks();
            Appointment appointment = new Appointment();

            PictureBox pictureBox = commonUtil.addLoaderImage(this.btn_save.Location.X + 205, this.btn_save.Location.Y + 2);
            this.Controls.Add(pictureBox);
            this.btn_save.Enabled = false;
            bool task = await Task.Run(() => this.DoValidations());
            UserEvent userEvent = this.GenerateUserEvent();
            if (task)
            {

                bool contact = false;
                contact = await Task.Run(() => eventHelper.UpdateEvent(userEvent));
                if (contact)
                {
                    this.Controls.Remove(pictureBox);
                    this.Close();
                }
                else
                {
                    Banner banner = new Banner();
                    banner = uiMessage.AddBanner("Unable to add Event. Please try again later", "error");
                    this.Controls.Add(banner);
                    banner.BringToFront();
                    this.Controls.Remove(pictureBox);
                    this.btn_save.Enabled = true;
                }
            }
            else
            {
                this.Controls.Remove(pictureBox);
                this.btn_save.Enabled = true;
            }
        }

        private void cmb_repeattype_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox dynamictxt_addressline1 = Controls.Find("dynamictxt_addressline1", true).FirstOrDefault() as TextBox;
            if (dynamictxt_addressline1 == null)
            {
                this.Size = new Size(627, 600);

            }
            else
            {
                this.Size = new Size(627, 750);

            }
            Label lbl_repeatfor = Controls.Find("lbl_repeatfor", true).FirstOrDefault() as Label;
            ComboBox cmb_repeatfor = Controls.Find("cmb_repeatfor", true).FirstOrDefault() as ComboBox;
            if (lbl_repeatfor != null && cmb_repeatfor != null)
            {

                lbl_repeatfor.Dispose();
                cmb_repeatfor.Dispose();
            }


            if (!cmb_repeattype.Text.Equals("") && !cmb_repeattype.Text.Equals("None"))
            {
                Label label = new Label()
                {
                    Name = "lbl_repeatfor",
                    Text = "Repeat For",
                    Location = new Point(41, this.cmb_repeattype.Location.Y + 30),
                    ForeColor = System.Drawing.Color.White,
                };

                ComboBox combo = new ComboBox()
                {
                    Name = "cmb_repeatfor",
                    Location = new Point(41, label.Location.Y + 22),
                    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25))))),
                    ForeColor = System.Drawing.Color.White,
                    Size = new System.Drawing.Size(250, 21),

                };
                combo.Items.AddRange(new object[] {
                    "Forever",
                    "Specific Number Of Times",
                    "Until",
                });
                combo.SelectedItem = userEvent.RepeatDuration;
                combo.SelectedIndexChanged += new System.EventHandler(this.cmb_repeatfor_SelectedIndexChanged);
                Controls.Add(label);
                Controls.Add(combo);

                if (cmb_repeatfor == null)
                {
                    cmb_repeatfor = Controls.Find("cmb_repeatfor", true).FirstOrDefault() as ComboBox;
                    btn_save.Location = new Point(41, cmb_repeatfor.Location.Y + cmb_repeatfor.Height + 10);

                }
            }
        }


        private void cmb_repeatfor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label lbl_duration = Controls.Find("lbl_repeatduration", true).FirstOrDefault() as Label;
            ComboBox cmb_repeatfor = Controls.Find("cmb_repeatfor", true).FirstOrDefault() as ComboBox;

            TextBox txt_duration = Controls.Find("txt_duration", true).FirstOrDefault() as TextBox;
            DateTimePicker dtp_duration = Controls.Find("dtp_duration", true).FirstOrDefault() as DateTimePicker;

            if (lbl_duration != null)
            {
                lbl_duration.Dispose();
            }

            if (txt_duration != null)
            {
                txt_duration.Dispose();
            }

            if (dtp_duration != null)
            {
                dtp_duration.Dispose();
            }

            if (!cmb_repeatfor.Text.Equals("") && !cmb_repeatfor.Text.Equals("None") && !cmb_repeatfor.Text.Equals("Forever"))
            {
                Label label = new Label()
                {
                    Name = "lbl_repeatduration",
                    Text = "Duration",
                    Location = new Point(333, this.cmb_repeattype.Location.Y + 30),
                    ForeColor = System.Drawing.Color.White,
                };

                Controls.Add(label);
            }

            if (cmb_repeatfor.Text.Equals("Specific Number Of Times"))
            {
                TextBox text = new TextBox()
                {
                    Name = "txt_duration",
                    Location = new Point(333, cmb_repeatfor.Location.Y),
                    ForeColor = System.Drawing.Color.White,
                    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31))))),
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F),
                    Text = userEvent.RepeatCount.ToString(),

                };
                Controls.Add(text);
            }

            if (cmb_repeatfor.Text.Equals("Until"))
            {
                DateTimePicker text = new DateTimePicker()
                {
                    Name = "dtp_duration",
                    Location = new Point(333, cmb_repeatfor.Location.Y),
                    ForeColor = System.Drawing.Color.White,
                    Value = userEvent.RepeatTill == DateTime.MaxValue ? DateTime.Now : userEvent.RepeatTill,
                };
                Controls.Add(text);
            }


        }

    }



}
