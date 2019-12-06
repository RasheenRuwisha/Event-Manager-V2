﻿using EventManager.DatabaseHelper;
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

namespace EventManager.View.Contacts
{
    public partial class AddContact : Form
    {
        readonly UiBuilder uiBuilder = new UiBuilder();
        readonly UiMessageUtitlity uiMessage = new UiMessageUtitlity();
        private Bitmap bitmap = new Bitmap(Properties.Resources.user);
        readonly FieldValidator fieldValidator = new FieldValidator();
        readonly CommonUtil commonUtil = new CommonUtil();

        public AddContact()
        {
            InitializeComponent();

            this.lbl_header.AutoSize = false;
            this.lbl_header.Left = this.Width / 2 - this.lbl_header.Width / 2;

            this.btn_save.AutoSize = false;
            this.btn_save.Left = this.Width / 2 - this.btn_save.Width / 2;

            this.txt_name.AutoSize = false;
            this.txt_name.Size = new System.Drawing.Size(250, 30);

            this.txt_email.AutoSize = false;
            this.txt_email.Size = new System.Drawing.Size(250, 30);

            this.txt_phone.AutoSize = false;
            this.txt_phone.Size = new System.Drawing.Size(250, 30);

            PictureBox pbx = uiBuilder.GeneratePictureBox(17, 293, "dynamicpbx_chevdown", Properties.Resources.chevdown, 15, 15);
            pbx.Click += new EventHandler(this.AddUiClick);
            this.Controls.Add(pbx);

            Label label = uiBuilder.GenerateLabel(40, 295, "dynamiclbl_address", "Address");
            this.Controls.Add(label);


            this.cpb_userimage.Left = this.Width / 2 - this.cpb_userimage.Width / 2;
            this.cpb_userimage.SizeMode = PictureBoxSizeMode.StretchImage;
            this.cpb_userimage.Image = bitmap;
            bitmap.Dispose();
        }


         private void AddAddressControls()
        {
            PictureBox tbx = this.Controls.Find("dynamicpbx_chevdown", true).FirstOrDefault() as PictureBox;
            Label label = this.Controls.Find("dynamiclbl_address", true).FirstOrDefault() as Label;

            if (tbx != null)
            {
                tbx.Dispose();
                label.Dispose();
            }
            this.Size = new Size(627, 470);
            PictureBox pbx = uiBuilder.GeneratePictureBox(17, 293, "dynamicpbx_chevup", Properties.Resources.chevup, 15, 15);
            pbx.Click += new EventHandler(this.RemoveUiClick);
            this.Controls.Add(pbx);
            this.Controls.Add(uiBuilder.GenerateLongTextBox(42, 310, "dynamictxt_addressline1", ""));
            this.Controls.Add(uiBuilder.GenerateLongTextBox(330, 310, "dynamictxt_addressline2", ""));
            this.Controls.Add(uiBuilder.GenerateShortTextBox(42, 372, "dynamictxt_city", ""));
            this.Controls.Add(uiBuilder.GenerateShortTextBox(243, 372, "dynamictxt_state", ""));
            this.Controls.Add(uiBuilder.GenerateShortTextBox(451, 372, "dynamictxt_zip", ""));
            this.Controls.Add(uiBuilder.GenerateLabel(40, 293, "dynamiclbl_addressline1", "Address Line 1 "));
            this.Controls.Add(uiBuilder.GenerateLabel(329, 293, "dynamiclbl_addressline2", "Address Line 2 "));
            this.Controls.Add(uiBuilder.GenerateLabel(40, 355, "dynamiclbl_city", "City "));
            this.Controls.Add(uiBuilder.GenerateLabel(241, 355, "dynamiclbl_state", "State "));
            this.Controls.Add(uiBuilder.GenerateLabel(449, 355, "dynamiclbl_zip", "Zip "));
            this.btn_save.Location = new Point(181, 427);
            this.btn_save.Left = this.Width / 2 - this.btn_save.Width / 2;
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
            this.Size = new Size(627, 378);
            PictureBox pbx = uiBuilder.GeneratePictureBox(17, 293, "dynamicpbx_chevdown", Properties.Resources.chevdown, 15, 15);
            pbx.Click += new EventHandler(this.AddUiClick);

            Label label = uiBuilder.GenerateLabel(40, 295, "dynamiclbl_address", "Address");

            this.btn_save.Location = new Point(194, 335);
            this.btn_save.Left = this.Width / 2 - this.btn_save.Width / 2;
            this.CenterToParent();
            this.Controls.Add(pbx);
            this.Controls.Add(label);
            return true;
        }

        private void RemoveUiClick(object sender, EventArgs e)
        {
            if (this.GetDynamicTextBoxValues("dynamictxt_addressline1").Equals("") && this.GetDynamicTextBoxValues("dynamictxt_addressline2").Equals("") &&
                this.GetDynamicTextBoxValues("dynamictxt_city").Equals("") && this.GetDynamicTextBoxValues("dynamictxt_state").Equals("") &&
                this.GetDynamicTextBoxValues("dynamictxt_zip").Equals(""))
            {
                this.RemoveDynamicUis();
            }
            else
            {
                var confirmResult = MessageBox.Show("Address fields will be removed from the contact. Do you want to proceed?",
                   "Confirm Delete!!",
                   MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    this.RemoveDynamicUis();
                }
            }

        }

        private void AddUiClick(object sender, EventArgs e)
        {
            this.AddAddressControls();
        }

        private void pb_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cpb_userimage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bitmap = new Bitmap(openFileDialog.FileName);
                cpb_userimage.Image = bitmap;
            }
        }


        private void ShowErrors()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    if(control.Name.Equals("txt_email") || control.Name.Equals("txt_name"))
                    {
                        if (String.IsNullOrEmpty(control.Text.Trim()))
                        {
                            PictureBox pictureBox = Controls.Find("ptx_" + control.Name, true).FirstOrDefault() as PictureBox;
                            if (pictureBox == null)
                            {
                                using (PictureBox error = uiMessage.AddErrorIcon(control.Name, control.Location.X + 255, control.Location.Y + 2))
                                {
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
                        }
                        else
                        {
                            PictureBox pictureBox = Controls.Find("ptx_" + control.Name, true).FirstOrDefault() as PictureBox;
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

        private bool CheckExistingEmail()
        {

            if (ContactHelper.DoesEmailExist(txt_email.Text.Trim()))
            {
                return true;
            }
            this.AddEmailErrorIcon();
            return false;
        }



        private bool CheckExistingName()
        {
            if (ContactHelper.DoesNameExist(txt_name.Text.Trim()))
            {
                return true;
            }
            this.AddNameErrorIcon();
            return false;
        }



        public void AddEmailErrorIcon()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.AddEmailErrorIcon));
            }
            else
            {
                Banner banner = uiMessage.AddBanner("Contact with email already exists", "error");
                banner.BringToFront();
                this.Controls.Add(banner);
                PictureBox error = uiMessage.AddErrorIcon(txt_email.Name, txt_email.Location.X + 255, txt_email.Location.Y + 2);
                this.Controls.Add(error);
            }
        }

        public void AddNameErrorIcon()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.AddEmailErrorIcon));
            }
            else
            {
                Banner banner = uiMessage.AddBanner("Contact with name already exists", "error");
                banner.BringToFront();
                this.Controls.Add(banner);
                PictureBox error = uiMessage.AddErrorIcon(txt_email.Name, txt_email.Location.X + 255, txt_email.Location.Y + 2);
                this.Controls.Add(error);
            }
        }

        private bool ValidateEmail()
        {
            if (fieldValidator.IsValidEmailAddress(txt_email.Text.Trim()))
            {
                return true;
            }
            this.AddEmailError();
            return false;
        }

        private void AddEmailError()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.AddEmailError));
            }
            else
            {
                PictureBox error = uiMessage.AddErrorIcon(txt_email.Name, txt_email.Location.X + 255, txt_email.Location.Y + 2);
                this.Controls.Add(error);
            }
        }

        private bool DoValidations()
        {
            return this.ValidateFields() && this.ValidateEmail() && this.CheckExistingEmail() && this.CheckExistingName();
        }

        private async void btn_save_Click(object sender, EventArgs e)
        {
            Contact contactDetails = this.GenerateContactObject();
            PictureBox pictureBox = commonUtil.addLoaderImage(this.btn_save.Location.X + 205, this.btn_save.Location.Y + 2);
            this.Controls.Add(pictureBox);
            this.btn_save.Enabled = false;
            bool task = await Task.Run(() => this.DoValidations());
            if (task)
            {
                bool contact = await Task.Run(()=> ContactHelper.AddContact(contactDetails));
                if (contact)
                {
                    this.Controls.Remove(pictureBox);
                    this.Close();
                }
                else
                {
                    Banner banner = new Banner();
                    banner = uiMessage.AddBanner("Unable to add contact. Please try again later", "error");
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

        private Contact GenerateContactObject()
        {
            Contact contact = new Contact()
            {
                ContactId = commonUtil.generateUserId("contact"),
                Name = txt_name.Text.Trim(),
                Email = txt_email.Text.Trim(),
                Phone = txt_phone.Text.Trim(),
                Image = commonUtil.BitmapToBase64(cpb_userimage.Image),
                AddressLine1 = this.GetDynamicTextBoxValues("dynamictxt_addressline1"),
                AddressLine2 = this.GetDynamicTextBoxValues("dynamictxt_addressline2"),
                State = this.GetDynamicTextBoxValues("dynamictxt_state"),
                City = this.GetDynamicTextBoxValues("dynamictxt_city"),
                Zipcode = this.GetDynamicTextBoxValues("dynamictxt_zip"),
                UserId = Application.UserAppDataRegistry.GetValue("userID").ToString()
            };
            return contact;
        }

    }
}
