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
        readonly UiBuilder uiBuilder = new UiBuilder();
        readonly UiMessageUtitlity uiMessage = new UiMessageUtitlity();
        readonly CommonUtil commonUtil = new CommonUtil();
        readonly String userId = Application.UserAppDataRegistry.GetValue("userID").ToString();


        List<Contact> contacts = new List<Contact>();
        List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();
        UserEvent userEvent = new UserEvent();
        List<Contact> allContacts = new List<Contact>();

        bool doesEventRepeat = false;

        public EditEvent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method intiializes the form with the details that are already existing in the event
        /// </summary>
        /// <param name="eventid"></param>
        public EditEvent(UserEvent  eventid)
        {
            InitializeComponent();

            this.userEvent = eventid;
            txt_name.Text = userEvent.Title;
            txt_notes.Text = userEvent.Description;
            dtp_startdate.Value = userEvent.StartDate;
            dtp_starttime.Value = userEvent.StartDate;
            dtp_enddate.Value = userEvent.StartDate;
            dtp_endtime.Value = userEvent.EndDate;

            cmb_repeattype.SelectedIndex = 0;

            if(userEvent.RepeatType.Equals("Daily") || userEvent.RepeatType.Equals("Weekly") || userEvent.RepeatType.Equals("Monthly"))
            {
                doesEventRepeat = true;
            }

            allContacts = ContactHelper.GetUserContacts();
            allContacts.RemoveAll(x => contacts.Exists(y => y.ContactId == x.ContactId));

            if(userEvent.EventContacts != null)
            {
                foreach(EventContact eventContact in userEvent.EventContacts)
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem()
                    {
                        Id  = eventContact.Id,
                        ContactId = eventContact.ContactId,
                        Name = this.GetContactName(eventContact.ContactId),
                    };
                    cmb_evetncollab.DisplayMember = "Name";
                    cmb_evetncollab.ValueMember = "ContactId";
                    cmb_evetncollab.DisplayMember = "Name";
                    cmb_evetncollab.ValueMember = "ContactId";
                    cmb_evetncollab.Items.Add(comboBoxItem);
                    comboBoxItems.Add(comboBoxItem);

                }
                if (userEvent.EventContacts.Count != 0)
                {
                    cmb_evetncollab.SelectedItem = 0;
                }
      
            }

            if (userEvent.Type.Equals("Task"))
            {
                rb_task.Checked = true;
            }
            else
            {
                rb_appointment.Checked = true;
            }

           allContacts.RemoveAll(x => userEvent.EventContacts.Exists(y => y.ContactId == x.ContactId));

            if (allContacts != null)
            {
                foreach (Contact eventContact in allContacts)
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem()
                    {
                        ContactId = eventContact.ContactId,
                        Name = eventContact.Name,
                    };

                    cmb_contacts.DisplayMember = "Name";
                    cmb_contacts.ValueMember = "ContactId";
                    cmb_contacts.DisplayMember = "Name";
                    cmb_contacts.ValueMember = "ContactId";
                    cmb_contacts.Items.Add(comboBoxItem);
                }
                cmb_contacts.Items.Remove("Loading....");
                if(allContacts.Count != 0)
                {
                    cmb_contacts.SelectedIndex = 0;
                }
                else
                {
                    cmb_contacts.Items.Add("No Contacts");
                    cmb_contacts.SelectedIndex = 0;
                }

            }

            if (!userEvent.AddressLine1.Equals("") || !userEvent.AddressLine2.Equals("") || !userEvent.City.Equals("") || !userEvent.State.Equals("") || !userEvent.Zipcode.Equals(""))
            {
                this.AddAddressControls();
                this.changeControlLocations("add");
            }
            else
            {
                PictureBox pbx = uiBuilder.GeneratePictureBox(17, 390, "dynamicpbx_chevdown", Properties.Resources.chevdown, 15, 15);
                pbx.Click += new EventHandler(this.AddUiClick);

                this.Controls.Add(pbx);

                Label label = uiBuilder.GenerateLabel(40, 390, "dynamiclbl_address", "Address");
                this.Controls.Add(label);
            }

            cmb_repeattype.SelectedItem = userEvent.RepeatType;

            this.dtp_starttime.ValueChanged += new EventHandler(startPickerValueChanged);
            this.dtp_startdate.ValueChanged += new EventHandler(startDatePickerValueChanged);
            this.dtp_enddate.ValueChanged += new EventHandler(endDate_ValueChanged);
            this.dtp_endtime.ValueChanged += new EventHandler(endTime_ValueChanged);

            this.dtp_startdate.ValueChanged += new EventHandler(DisableRepeatTypes);
            this.dtp_enddate.ValueChanged += new EventHandler(DisableRepeatTypes);

        }

        /// <summary>
        /// This method disables the repeat types according the changes done to the start and end date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DisableRepeatTypes(object sender, EventArgs e)
        {
            if ((dtp_enddate.Value - dtp_startdate.Value).Days >= 30)
            {
                cmb_repeattype.Items.Remove("Monthly");
                cmb_repeattype.Items.Remove("Weekly");
                cmb_repeattype.Items.Remove("Daily");
                cmb_repeattype.SelectedIndex = 0;
            }
            else
            {
                if (!cmb_repeattype.Items.Contains("Daily"))
                {
                    cmb_repeattype.Items.Add("Daily");
                }
                if (!cmb_repeattype.Items.Contains("Weekly"))
                {
                    cmb_repeattype.Items.Add("Weekly");
                }
                if (!cmb_repeattype.Items.Contains("Monthly"))
                {
                    cmb_repeattype.Items.Add("Monthly");
                }
            }

            if ((dtp_enddate.Value - dtp_startdate.Value).Days >= 7)
            {
                cmb_repeattype.Items.Remove("Weekly");
                cmb_repeattype.Items.Remove("Daily");
            }
            else
            {
                if (!cmb_repeattype.Items.Contains("Daily"))
                {
                    cmb_repeattype.Items.Add("Daily");
                }
                if (!cmb_repeattype.Items.Contains("Weekly"))
                {
                    cmb_repeattype.Items.Add("Weekly");
                }
                cmb_repeattype.SelectedIndex = 0;

            }


            if ((dtp_enddate.Value - dtp_startdate.Value).Days >= 1)
            {
                cmb_repeattype.Items.Remove("Daily");
            }
            else
            {
                if (!cmb_repeattype.Items.Contains("Daily"))
                {
                    cmb_repeattype.Items.Add("Daily");
                }
                cmb_repeattype.SelectedIndex = 0;
            }


        }


        /// <summary>
        /// This method checks weteher the end time is less than the start time and changes the values of the end time accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void startPickerValueChanged(object sender, EventArgs e)
        {
            if (dtp_endtime.Value < dtp_starttime.Value)
            {
                this.dtp_endtime.Value = this.dtp_starttime.Value.AddHours(1);
            }

        }

        /// <summary>
        /// This checks wether the end time is less than the start date and changes the value of the end date to the star date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void startDatePickerValueChanged(object sender, EventArgs e)
        {
            if (dtp_enddate.Value < dtp_startdate.Value)
            {
                this.dtp_enddate.Value = this.dtp_startdate.Value;

            }
        }

        /// <summary>
        /// This checks wether the end date is less than the start date and changes the end date to the star date even if the user selects a past date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_enddate.Value < dtp_startdate.Value)
            {
                dtp_enddate.Value = dtp_startdate.Value;
            }
        }


        /// <summary>
        /// This checks wether the end time is less than the start time and changes the end time to the start dtimeate even if the user selects a past time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endTime_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_endtime.Value < dtp_starttime.Value)
            {
                dtp_endtime.Value = dtp_starttime.Value;
            }
        }

        /// <summary>
        /// This method gets the name of the contact using the contact id
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public String GetContactName(String contactId)
        {
            String name = "";
            foreach(Contact contact in allContacts)
            {
                if (contact.ContactId.Equals(contactId))
                {
                    name =  contact.Name;
                    break;
                }
            }

            return name;
        }


        /// <summary>
        /// This method changes the location of the controls when the dynamic views are added.
        /// </summary>
        /// <param name="type"></param>
        private void changeControlLocations(string type)
        {
            Label lbl_repeatduration = Controls.Find("lbl_repeatduration", true).FirstOrDefault() as Label;
            ComboBox cmb_repeatfor = Controls.Find("cmb_repeatfor", true).FirstOrDefault() as ComboBox;
            TextBox txt_duration = Controls.Find("txt_duration", true).FirstOrDefault() as TextBox;
            DateTimePicker dtp_duration = Controls.Find("dtp_duration", true).FirstOrDefault() as DateTimePicker;
            Label lbl_repeatfor = Controls.Find("lbl_repeatfor", true).FirstOrDefault() as Label;

            if (type.Equals("add"))
            {
                lbl_contacts.Location = new Point(41, 512);
                cmb_contacts.Location = new Point(41, lbl_contacts.Location.Y + lbl_contacts.Height + 10);
                lbl_collaborators.Location = new Point(335, lbl_contacts.Location.Y );
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
                lbl_contacts.Location = new Point(41, 421);
                cmb_contacts.Location = new Point(41, lbl_contacts.Location.Y + lbl_contacts.Height + 10);
                lbl_collaborators.Location = new Point(335, lbl_contacts.Location.Y);
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
                    else if (dtp_duration != null)
                    {
                        dtp_duration.Location = new Point(333, cmb_repeatfor.Location.Y);
                    }

                }
            }
        }

        /// <summary>
        /// This method adds the address controls when the user click on the chevron and then adjusts the location of the existing controls.
        /// </summary>
        private void AddAddressControls()
        {
            PictureBox tbx = this.Controls.Find("dynamicpbx_chevdown", true).FirstOrDefault() as PictureBox;
            Label label = this.Controls.Find("dynamiclbl_address", true).FirstOrDefault() as Label;

            if (tbx != null)
            {
                tbx.Dispose();
                label.Dispose();
            }

            Label rLabel = Controls.Find("lbl_repeatfor", true).FirstOrDefault() as Label;
            if (rLabel != null)
            {
                this.Size = new Size(627, 750);
            }
            else
            {
                this.Size = new Size(627, 700);
            }
            this.Controls.Add(uiBuilder.GenerateLongTextBox(42, 410, "dynamictxt_addressline1", userEvent.AddressLine1,50,9));
            this.Controls.Add(uiBuilder.GenerateLongTextBox(330, 410, "dynamictxt_addressline2", userEvent.AddressLine2,50,10));
            this.Controls.Add(uiBuilder.GenerateShortTextBox(42, 467, "dynamictxt_city", userEvent.City ,50,11));
            this.Controls.Add(uiBuilder.GenerateShortTextBox(243, 467, "dynamictxt_state", userEvent.State ,50,12));
            this.Controls.Add(uiBuilder.GenerateShortTextBox(451, 467, "dynamictxt_zip", userEvent.Zipcode,10,13));
            this.Controls.Add(uiBuilder.GenerateLabel(40, 388, "dynamiclbl_addressline1", "Address Line 1 "));
            this.Controls.Add(uiBuilder.GenerateLabel(329, 388, "dynamiclbl_addressline2", "Address Line 2 "));
            this.Controls.Add(uiBuilder.GenerateLabel(40, 445, "dynamiclbl_city", "City "));
            this.Controls.Add(uiBuilder.GenerateLabel(241, 445, "dynamiclbl_state", "State "));
            this.Controls.Add(uiBuilder.GenerateLabel(449, 445, "dynamiclbl_zip", "Zip "));

            PictureBox pbx = uiBuilder.GeneratePictureBox(17, 390, "dynamicpbx_chevup", Properties.Resources.chevup, 15, 15);
            pbx.Click += new EventHandler(this.RemoveUiClick);
            this.Controls.Add(pbx);

            this.CenterToParent();
        }

        /// <summary>
        /// This methods retreives the values from the dynamic controls
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private String GetDynamicTextBoxValues(String name)
        {
            TextBox tbx = this.Controls.Find(name, true).FirstOrDefault() as TextBox;
            if (tbx != null)
            {
                return tbx.Text.Trim();
            }
            return "";
        }


        /// <summary>
        /// This method removes the dynamically added fields and then readjusts the location of the other controls.
        /// </summary>
        /// <returns></returns>
        private bool RemoveDynamicUis()
        {
            Label rLabel = Controls.Find("lbl_repeatfor", true).FirstOrDefault() as Label;
            if (rLabel != null)
            {
                this.Size = new Size(627, 600);
            }
            else
            {
                this.Size = new Size(627, 626);
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
            PictureBox pbx = uiBuilder.GeneratePictureBox(17, 390, "dynamicpbx_chevdown", Properties.Resources.chevdown, 15, 15);
            pbx.Click += new EventHandler(this.AddUiClick);

            Label label = uiBuilder.GenerateLabel(40, 390, "dynamiclbl_address", "Address");
            this.changeControlLocations("");
            this.CenterToParent();
            this.Controls.Add(pbx);
            this.Controls.Add(label);
            return true;
        }


        /// <summary>
        /// This method handels the addtion of the Dynamic uis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUiClick(object sender, EventArgs e)
        {
            this.AddAddressControls();
            this.changeControlLocations("add");
        }

        /// <summary>
        /// This method handles the click event for removing the dynamic ui.
        /// If fields in the dynamic ui's are filled a confimration will be shown to the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveUiClick(object sender, EventArgs e)
        {
            if (this.GetDynamicTextBoxValues("dynamictxt_addressline1").Equals("") && this.GetDynamicTextBoxValues("dynamictxt_addressline2").Equals("") &&
                this.GetDynamicTextBoxValues("dynamictxt_city").Equals("") && this.GetDynamicTextBoxValues("dynamictxt_state").Equals("") &&
                this.GetDynamicTextBoxValues("dynamictxt_zip").Equals(""))
            {
                this.RemoveDynamicUis();
            }
            else {
                var confirmResult = MessageBox.Show("Address fields will be removed from the contact. Do you want to proceed?",
                       "Confirm Delete!!",
                       MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    this.RemoveDynamicUis();
                }
            }

        }

        private void pb_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// This methods add the contacts to the contact list.
        /// </summary>

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
                            ContactId = contact.ContactId,
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

        /// <summary>
        /// This method adds the contact to the collaborator list and then removes the contact from the contact list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_addcollab_Click(object sender, EventArgs e)
        {
            if (cmb_contacts.Text.Equals("") && cmb_evetncollab.Text.Equals("No More Contacts"))
            {
                MessageBox.Show("Please Select an contact!");
            }
            else
            {
                ComboBoxItem comboBoxItem = cmb_contacts.SelectedItem as ComboBoxItem;
                comboBoxItems.Add(comboBoxItem);
                cmb_evetncollab.Items.Add(comboBoxItem);
                cmb_contacts.Items.Remove(comboBoxItem);
                btn_removecollab.Enabled = true;
                cmb_evetncollab.Items.Remove("No Contacts Added");

                if (cmb_contacts.Items.Count == 0)
                {
                    cmb_contacts.Items.Add("No More Contacts");
                    lbl_addcollab.Enabled = false;
                    cmb_contacts.SelectedIndex = 0;
                }
                else
                {
                    lbl_addcollab.Enabled = true;
                    cmb_contacts.SelectedIndex = 0;
                }

            }
        }

        /// <summary>
        /// This method removes the contact from thr collaborators list and adds the contact back to the contact list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_removecollab_Click(object sender, EventArgs e)
        {
            if (cmb_evetncollab.Text.Equals("") && cmb_evetncollab.Text.Equals("No Contacts Added"))
            {
                MessageBox.Show("Please Select an contact!");
            }
            else
            {
                ComboBoxItem comboBoxItem = cmb_evetncollab.SelectedItem as ComboBoxItem;
                cmb_contacts.Items.Add(comboBoxItem);
                cmb_evetncollab.Items.Remove(comboBoxItem);
                comboBoxItems.Remove(comboBoxItem);
                lbl_addcollab.Enabled = true;
                cmb_contacts.Items.Remove("No More Contacts");

                if (cmb_evetncollab.Items.Count == 0)
                {
                    cmb_evetncollab.Items.Add("No Contacts Added");
                    btn_removecollab.Enabled = false;
                    cmb_evetncollab.SelectedIndex = 0;
                }
                else
                {
                    btn_removecollab.Enabled = true;
                    cmb_evetncollab.SelectedIndex = 0;
                }
            }
        }


        /// <summary>
        /// This methods add the error icons next to the mandataory fields if they are left blank.
        /// </summary>
        private void ShowErrors()
        {
            PictureBox pictureBox = Controls.Find("ptx_" + txt_name.Name, true).FirstOrDefault() as PictureBox;

            if (String.IsNullOrEmpty(txt_name.Text.Trim()))
            {
                if (pictureBox == null)
                {
                    PictureBox error = uiMessage.AddErrorIcon(txt_name.Name, txt_name.Location.X + 255, txt_name.Location.Y + 2);
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


        /// <summary>
        /// This method validates the fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateFields()
        {
            this.ShowErrors();
            if (txt_name.Text.Trim().Equals(""))
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


        /// <summary>
        /// This method generates the contacts for the event using the data from the collaborators combo box
        /// </summary>
        /// <returns></returns>
        private List<EventContact> GenerateEventContacts()
        {
            List<EventContact> eventContacts = new List<EventContact>();
            foreach (ComboBoxItem cmb in comboBoxItems)
            {
                EventContact eventContact = new EventContact()
                {
                    ContactId = cmb.ContactId,
                    ContactName = cmb.Name,
                    EventId = userEvent.EventId,
                    UserId = userId,
                };

                eventContacts.Add(eventContact);
            }
            return eventContacts;
        }

        /// <summary>
        /// This methods generates the End time according to the repeat type that is slected by the user.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// This method generates the event that is nedded to be created.
        /// </summary>
        /// <returns></returns>
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
                EventId = userEvent.EventId,
                UserId = Application.UserAppDataRegistry.GetValue("userID").ToString(),
                Title = txt_name.Text,
                Description = txt_notes.Text,
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
                events.Type = "Appointment";
            }
            else
            {
                events.Type = "Task";
            }

            return events;
        }

        /// <summary>
        /// This method handles the save function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_save_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = commonUtil.AddLoaderImage(this.btn_save.Location.X + 205, this.btn_save.Location.Y + 2);
            this.Controls.Add(pictureBox);
            this.btn_save.Enabled = false;
            bool task = await Task.Run(() => this.DoValidations());
            UserEvent userEvent = this.GenerateUserEvent();
            if (task)
            {

                bool contact = false;
                if (doesEventRepeat)
                {
                    RepeatEventConfirmation repeatEventConfirmation = new RepeatEventConfirmation(userEvent, this);
                    repeatEventConfirmation.ShowDialog();
                }
                else
                {
                    contact = await Task.Run(() => EventHelper.UpdateEvent(userEvent));
                    if (contact)
                    {
                        this.Controls.Remove(pictureBox);
                        Notification notification = new Notification("Event Updated Successfully");
                        Timer timer = new Timer();
                        notification.Show();

                        timer.Tick += (o, ea) =>
                        {
                            notification.Close();
                            this.Close();
                        };

                        timer.Interval = 1000;
                        timer.Start();


                    }
                }
               
          
            }
            else
            {
                this.Controls.Remove(pictureBox);
                this.btn_save.Enabled = true;
            }
        }

        /// <summary>
        /// This methods checks for the change in the repeat type and adds the repeat for section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_repeattype_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox dynamictxt_addressline1 = Controls.Find("dynamictxt_addressline1", true).FirstOrDefault() as TextBox;
            if (dynamictxt_addressline1 == null)
            {
                this.Size = new Size(627, 650);

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
                combo.SelectedIndex = 0;

                Controls.Add(label);
                Controls.Add(combo);
                combo.SelectedIndexChanged += new System.EventHandler(this.cmb_repeatfor_SelectedIndexChanged);
                combo.SelectedItem = userEvent.RepeatDuration;
                if (cmb_repeatfor == null)
                {
                    cmb_repeatfor = Controls.Find("cmb_repeatfor", true).FirstOrDefault() as ComboBox;
                    btn_save.Location = new Point(41, cmb_repeatfor.Location.Y + cmb_repeatfor.Height + 10);

                }
            }
            else
            {
                Label lbl_duration = Controls.Find("lbl_repeatduration", true).FirstOrDefault() as Label;
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
            }
        }


        /// <summary>
        /// This methods checks for the changes in the repeat for method and then adds the required control for the user to add the details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    Text = (userEvent.RepeatCount+1).ToString(),

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
                    Value = userEvent.RepeatTill.Date == DateTime.MaxValue.Date ? DateTime.Now : userEvent.RepeatTill.AddHours(24).AddSeconds(-1),
                };
                Controls.Add(text);
            }


        }

    }



}
