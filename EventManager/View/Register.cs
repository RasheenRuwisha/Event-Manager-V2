﻿using EventManager.DatabaseHelper;
using EventManager.Model;
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
using System.Threading.Tasks;
namespace EventManager.View
{
    public partial class Register : Form
    {
        Bitmap bitmap = new Bitmap(Properties.Resources.user);
        UiMessageUtitlity uiMessageUtitlity = new UiMessageUtitlity();
        CommonUtil commonUtil = new CommonUtil();
        FieldValidator fieldValidator = new FieldValidator();
        UserHelper userHelper = new UserHelper();

        public Register()
        {
            InitializeComponent();

            this.pb_logo.Image = Properties.Resources.logo2;
            this.pb_logo.AutoSize = false;
            this.pb_logo.Left = this.Width / 2 - this.pb_logo.Width / 2;

            this.txt_username.AutoSize = false;
            this.txt_username.Size = new System.Drawing.Size(250, 30);

            this.txt_email.AutoSize = false;
            this.txt_email.Size = new System.Drawing.Size(250, 30);

            this.txt_firstname.AutoSize = false;
            this.txt_firstname.Size = new System.Drawing.Size(250, 30);

            this.txt_lastname.AutoSize = false;
            this.txt_lastname.Size = new System.Drawing.Size(250, 30);

            this.txt_password.AutoSize = false;
            this.txt_password.Size = new System.Drawing.Size(250, 30);


            this.txt_confirmpassword.AutoSize = false;
            this.txt_confirmpassword.Size = new System.Drawing.Size(250, 30);

            this.cpb_userimage.Left = this.Width / 2 - this.cpb_userimage.Width / 2;
            this.cpb_userimage.SizeMode = PictureBoxSizeMode.StretchImage;
            this.cpb_userimage.Image = bitmap;
        }



        // Add error icons next to the invalid texboxes
        /// <summary>
        /// Checks for all the texboxes which are empty once the user click on submmit and adds the error icons to the respective texboxes
        /// </summary>
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
                            PictureBox error = uiMessageUtitlity.AddErrorIcon(contorl.Name, contorl.Location.X + 255, contorl.Location.Y + 2);
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
            if (txt_username.Text.Trim().Equals("") || txt_email.Text.Trim().Equals("") ||
               txt_firstname.Text.Trim().Equals("") || txt_lastname.Text.Trim().Equals("") ||
               txt_password.Text.Trim().Equals("") || txt_confirmpassword.Text.Trim().Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Add error icons next to the invalid passwords
        /// <summary>
        /// Checks for the password similarity once user clicks submit and add error icon if the passwords do not match
        /// </summary>
        private bool ValidatePassword()
        {
            if (txt_password.Text.Trim().Equals(txt_confirmpassword.Text.Trim()) && !txt_password.Text.Trim().Equals("")
                && !txt_confirmpassword.Text.Trim().Equals(""))
            {
                if (txt_password.Text.Trim().Length <= 6)
                {
                    this.AddPasswordError();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                this.AddPasswordConfirmError();
                return false;
            }
        }

        private void AddPasswordError()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.AddPasswordError));
            }
            else
            {
                PictureBox error = uiMessageUtitlity.AddPasswordErrorIcon(txt_password.Name, txt_password.Location.X + 255, txt_password.Location.Y + 2);
                this.Controls.Add(error);
            }
        }

        private void AddPasswordConfirmError()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.AddPasswordConfirmError));
            }
            else
            {
                PictureBox error = uiMessageUtitlity.AddPasswordErrorIcon(txt_confirmpassword.Name, txt_confirmpassword.Location.X + 255, txt_confirmpassword.Location.Y + 2);
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

        private bool CheckExistingEmail()
        {
            if (userHelper.IsNewUser(txt_email.Text.Trim()))
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
                PictureBox error = uiMessageUtitlity.AddErrorIcon(txt_email.Name, txt_email.Location.X + 255, txt_email.Location.Y + 2);
                this.Controls.Add(error);
            }
        }

        private bool DoValidations()
        {
            return this.ValidateFields() && this.ValidateEmail()  && this.CheckExistingEmail() && this.ValidatePassword();
        }

        private async void btn_register_Click(object sender, EventArgs e)
        {

           
            String id = commonUtil.generateUserId("user");
            PictureBox picture = commonUtil.addLoaderImage(this.btn_register.Location.X + 205, this.btn_register.Location.Y + 2);
            btn_register.Enabled = false;
            Controls.Add(picture);
            User user = new User()
            {
                UserId = id,
                Email = txt_email.Text.Trim(),
                Username = txt_username.Text.Trim(),
                Firstname = txt_firstname.Text.Trim(),
                Lastname = txt_lastname.Text.Trim(),
                Image = commonUtil.BitmapToBase64(cpb_userimage.Image)
            };

            UserCredential userCredential = new UserCredential()
            {
                UserId = id,
                Password = txt_password.Text.Trim(),
                Email = txt_email.Text.Trim(),
                Username = txt_username.Text.Trim()
            };
            bool task = await Task.Run(() => this.DoValidations());
            if (task)
            {
                bool register = await Task.Run(() => userHelper.AddUser(user, userCredential));
                if (task)
                {
                    Controls.Remove(picture);
                    Login login = new Login();
                    login.Show();
                    this.Close();
                }
                else
                {
                    this.Controls.Remove(picture);
                    this.btn_register.Enabled = true;
                }
            }
            else
            {
                this.Controls.Remove(picture);
                this.btn_register.Enabled = true;
            }

        }

        private void lbl_login_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
