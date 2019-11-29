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
    public partial class AddEvent : Form
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


        String eventid = "";
        public AddEvent()
        {
            InitializeComponent();
            this.dtp_endtime.Value = this.dtp_starttime.Value.AddHours(1);
            this.dtp_starttime.ValueChanged += new EventHandler(startPickerValueChanged);
            this.dtp_startdate.ValueChanged += new EventHandler(startDatePickerValueChanged);
            this.dtp_startdate.ValueChanged += new EventHandler(startDate_ValueChanged);
            this.dtp_enddate.ValueChanged += new EventHandler(endDate_ValueChanged);
            this.dtp_endtime.ValueChanged += new EventHandler(endTime_ValueChanged);

            this.lbl_header.AutoSize = false;
            this.lbl_header.Left = this.Width / 2 - this.lbl_header.Width / 2;


            this.txt_name.AutoSize = false;
            this.txt_name.Size = new System.Drawing.Size(250, 30);

            this.txt_email.AutoSize = false;
            this.txt_email.Size = new System.Drawing.Size(250, 30);
        }


        void startPickerValueChanged(object sender, EventArgs e)
        {
            if (dtp_endtime.Value < dtp_starttime.Value)
            {
                this.dtp_endtime.Value = this.dtp_starttime.Value.AddHours(1);
            }

        }

        void startDatePickerValueChanged(object sender, EventArgs e)
        {
            if (dtp_enddate.Value < dtp_startdate.Value)
            {
                this.dtp_enddate.Value = this.dtp_startdate.Value;

            }
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_startdate.Value < DateTime.Now)
            {
                dtp_startdate.Value = DateTime.Now;
            }
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_enddate.Value < dtp_startdate.Value)
            {
                dtp_enddate.Value = dtp_startdate.Value;
            }
        }
        private void endTime_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_endtime.Value < dtp_starttime.Value)
            {
                dtp_endtime.Value = dtp_starttime.Value;
            }
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
            if (rb_appointment.Checked)
            {
                lbl_collaborators.Location = new Point(41, 415);
                cmb_contacts.Location = new Point(41, lbl_collaborators.Location.Y + lbl_collaborators.Height + 10);
                lbl_addcollab.Location = new Point(299, cmb_contacts.Location.Y);
                cmb_evetncollab.Location = new Point(333, cmb_contacts.Location.Y);
                btn_removecollab.Location = new Point(589, cmb_contacts.Location.Y);
                lbl_repeat.Location = new Point(41, cmb_contacts.Location.Y + cmb_contacts.Height + 10);
                cmb_repeattype.Location = new Point(41, lbl_repeat.Location.Y + lbl_repeat.Height + 10);
                btn_save.Location = new Point(41, cmb_repeattype.Location.Y + cmb_repeattype.Height + 10);
            }
            else
            {
                lbl_collaborators.Location = new Point(41, 281);
                cmb_contacts.Location = new Point(41, lbl_collaborators.Location.Y + lbl_collaborators.Height + 10);
                lbl_addcollab.Location = new Point(299, cmb_contacts.Location.Y);
                cmb_evetncollab.Location = new Point(333, cmb_contacts.Location.Y);
                btn_removecollab.Location = new Point(589, cmb_contacts.Location.Y);
                lbl_repeat.Location = new Point(41, cmb_contacts.Location.Y + cmb_contacts.Height + 10);
                cmb_repeattype.Location = new Point(41, lbl_repeat.Location.Y + lbl_repeat.Height + 10);
                btn_save.Location = new Point(43, 420);
            }
        }


        private void AddAddressControls()
        {
            this.Size = new Size(627, 571);
            PictureBox tbx = this.Controls.Find("dynamicpbx_chevdown", true).FirstOrDefault() as PictureBox;
            if (tbx != null)
            {
                tbx.Dispose();
            }
            this.Controls.Add(uiBuilder.GenerateLongTextBox(42, 303, "dynamictxt_addressline1", ""));
            this.Controls.Add(uiBuilder.GenerateLongTextBox(330, 303, "dynamictxt_addressline2", ""));
            this.Controls.Add(uiBuilder.GenerateShortTextBox(42, 370, "dynamictxt_city", ""));
            this.Controls.Add(uiBuilder.GenerateShortTextBox(243, 370, "dynamictxt_state", ""));
            this.Controls.Add(uiBuilder.GenerateShortTextBox(451, 370, "dynamictxt_zip", ""));
            this.Controls.Add(uiBuilder.GenerateLabel(40, 281, "dynamiclbl_addressline1", "Address Line 1 "));
            this.Controls.Add(uiBuilder.GenerateLabel(329, 281, "dynamiclbl_addressline2", "Address Line 2 "));
            this.Controls.Add(uiBuilder.GenerateLabel(40, 348, "dynamiclbl_city", "City "));
            this.Controls.Add(uiBuilder.GenerateLabel(241, 348, "dynamiclbl_state", "State "));
            this.Controls.Add(uiBuilder.GenerateLabel(449, 348, "dynamiclbl_zip", "Zip "));
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
            this.Size = new Size(627, 461);
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
                    EventId = eventid,
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
            if(cComboBox != null)
            {
                if (cComboBox.Text.Equals("Specific Number Of Times"))
                {
                    if (cmb_repeattype.Text.Equals("Daily")) {
                        endDate = dtp_enddate.Value.Date.AddDays(Int32.Parse(durationText.Text)).AddSeconds(-1);

                    }
                    else if (cmb_repeattype.Text.Equals("Weekly"))
                    {
                        endDate = dtp_enddate.Value.Date.AddDays(Int32.Parse(durationText.Text) * 7).AddSeconds(-1);

                    }
                    else if(cmb_repeattype.Text.Equals("Monthly"))
                    {
                        endDate = dtp_enddate.Value.Date.AddMonths(Int32.Parse(durationText.Text)).AddSeconds(-1);

                    }
                }else if (cComboBox.Text.Equals("Forever"))
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
        private Appointment GenerateAppointmentObject()
        {
            ComboBox cComboBox = Controls.Find("cmb_repeatfor", true).FirstOrDefault() as ComboBox;
            TextBox durationText = Controls.Find("txt_duration", true).FirstOrDefault() as TextBox;
            DateTimePicker durationTime = Controls.Find("dtp_duration", true).FirstOrDefault() as DateTimePicker;

            Appointment appointment = new Appointment()
            {
                AddressLine1 = this.GetDynamicTextBoxValues("dynamictxt_addressline1"),
                AddressLine2 = this.GetDynamicTextBoxValues("dynamictxt_addressline2"),
                State = this.GetDynamicTextBoxValues("dynamictxt_state"),
                City = this.GetDynamicTextBoxValues("dynamictxt_city"),
                Zipcode = this.GetDynamicTextBoxValues("dynamictxt_zip"),
                eventid = this.eventid,
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

            return appointment;
        }

        private Tasks GenerateTaskObject()
        {
            ComboBox cComboBox = Controls.Find("cmb_repeatfor", true).FirstOrDefault() as ComboBox;
            TextBox durationText = Controls.Find("txt_duration", true).FirstOrDefault() as TextBox;
            DateTimePicker durationTime = Controls.Find("dtp_duration", true).FirstOrDefault() as DateTimePicker;

            Tasks appointment = new Tasks()
            {
                eventid = this.eventid,
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

            return appointment;
        }


        private async void btn_save_Click(object sender, EventArgs e)
        {
            eventid = commonUtil.generateUserId("event");
            bool isAppointment = false;
            Tasks tasks = new Tasks();
            Appointment appointment = new Appointment();
            if (rb_appointment.Checked)
            {
                appointment = this.GenerateAppointmentObject();
                isAppointment = true;
            }
            else
            {
                tasks = this.GenerateTaskObject();
            }

            PictureBox pictureBox = commonUtil.addLoaderImage(this.btn_save.Location.X + 205, this.btn_save.Location.Y + 2);
            this.Controls.Add(pictureBox);
            this.btn_save.Enabled = false;
            bool task = await Task.Run(() => this.DoValidations());
            if (task)
            {

                bool contact = false;
                if (isAppointment)
                {
                    contact = await Task.Run(() => eventHelper.AddAppointment(appointment));
                }
                else
                {
                    contact = await Task.Run(() => eventHelper.AddEvent(tasks));
                }
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
            Label rLabel = Controls.Find("lbl_repeatfor", true).FirstOrDefault() as Label;
            ComboBox cComboBox = Controls.Find("cmb_repeatfor", true).FirstOrDefault() as ComboBox;
            if(rLabel != null && cComboBox != null)
            {

                rLabel.Dispose();
                cComboBox.Dispose();
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
                combo.SelectedIndexChanged += new System.EventHandler(this.cmb_repeatfor_SelectedIndexChanged);
                Controls.Add(label);
                Controls.Add(combo);
            }
        }


        private void cmb_repeatfor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label rLabel = Controls.Find("lbl_repeatduration", true).FirstOrDefault() as Label;
            ComboBox cComboBox = Controls.Find("cmb_repeatfor", true).FirstOrDefault() as ComboBox;

            TextBox durationText = Controls.Find("txt_duration", true).FirstOrDefault() as TextBox;
            DateTimePicker durationTime = Controls.Find("dtp_duration", true).FirstOrDefault() as DateTimePicker;

            if (rLabel != null )
            {
                rLabel.Dispose();
            }

            if (durationText != null)
            {
                durationText.Dispose();
            }

            if (durationTime != null)
            {
                durationTime.Dispose();
            }

            if (!cComboBox.Text.Equals("") && !cComboBox.Text.Equals("None") && !cComboBox.Text.Equals("Forever"))
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

            if (cComboBox.Text.Equals("Specific Number Of Times"))
            {
                TextBox text = new TextBox()
                {
                    Name = "txt_duration",
                    Location = new Point(333, cComboBox.Location.Y),
                    ForeColor = System.Drawing.Color.White,
                    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31))))),
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F),

            };
                Controls.Add(text);
            }

            if (cComboBox.Text.Equals("Until"))
            {
                DateTimePicker text = new DateTimePicker()
                {
                    Name = "dtp_duration",
                    Location = new Point(333, cComboBox.Location.Y),
                    ForeColor = System.Drawing.Color.White,
                };
                Controls.Add(text);
            }


        }
    }
}
