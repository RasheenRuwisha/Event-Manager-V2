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

namespace EventManager.View
{
    public partial class ForgotPassword : Form
    {

        UiMessageUtitlity uiMessageUtitlity = new UiMessageUtitlity();
        CommonUtil commonUtil = new CommonUtil();
        Banner banner = new Banner();
        string otp;
        string window;
        public ForgotPassword()
        {
            InitializeComponent();
        }

        public ForgotPassword(string window)
        {
            InitializeComponent();
            this.window = window;

            this.logo.Image = Properties.Resources.logo;
            this.logo.AutoSize = false;
            this.logo.Left = this.Width / 2 - this.logo.Width / 2;

            this.txt_email.AutoSize = false;
            this.txt_email.Size = new System.Drawing.Size(250, 30);

            this.txt_password.AutoSize = false;
            this.txt_password.Size = new System.Drawing.Size(250, 30);

            this.txt_confirmpasword.AutoSize = false;
            this.txt_confirmpasword.Size = new System.Drawing.Size(250, 30);

            this.txt_otp.AutoSize = false;
            this.txt_otp.Size = new System.Drawing.Size(250, 30);

            this.email_panel.BringToFront();
            if (!window.Equals("Login"))
            {
                txt_email.Text = Application.UserAppDataRegistry.GetValue("email").ToString();
                txt_email.Enabled = false;
                Controls.Remove(lbl_login);
            }
        }

        private async void btn_send_mail_Click(object sender, EventArgs e)
        {
            if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
            {
                PictureBox picture = commonUtil.addLoaderImage(this.btn_send_mail.Location.X + 205, this.btn_send_mail.Location.Y + 2);
                btn_send_mail.Enabled = false;
                email_panel.Controls.Add(picture);
                if (banner != null)
                {
                    this.email_panel.Controls.Remove(banner);
                }

                if (txt_email.Text.Trim().Equals(""))
                {
                    PictureBox pictureBox = Controls.Find("ptx_" + this.txt_email, true).FirstOrDefault() as PictureBox;
                    if (pictureBox == null)
                    {
                        PictureBox error = uiMessageUtitlity.AddErrorIcon(this.txt_email.Name, this.txt_email.Location.X + 255, this.txt_email.Location.Y + 2);
                        this.email_panel.Controls.Add(error);
                    }
                }
                else
                {
                    PictureBox pictureBox = Controls.Find("ptx_" + this.txt_email, true).FirstOrDefault() as PictureBox;
                    if (pictureBox == null)
                    {
                        PictureBox error = uiMessageUtitlity.AddErrorIcon(this.txt_email.Name, this.txt_email.Location.X + 255, this.txt_email.Location.Y + 2);
                        this.email_panel.Controls.Remove(error);
                    }
                    if (UserHelper.UserExists(txt_email.Text.Trim()))
                    {
                        this.otp = commonUtil.generateOTP();
                        String t = await Task.Run(() => this.sendEmail());
                        if (t.Equals("success"))
                        {
                            banner = uiMessageUtitlity.AddBanner($"Email has been sent to {txt_email.Text.Trim()}", "success");
                            this.verification_panel.Controls.Add(banner); ;
                            this.verification_panel.BringToFront();
                        }
                        else
                        {
                            banner = uiMessageUtitlity.AddBanner("Unable to send email! Please try again later!", "error");
                            this.email_panel.Controls.Add(banner);
                        }
                    }
                    else
                    {
                        banner = uiMessageUtitlity.AddBanner("Email Not Found", "error");
                        this.email_panel.Controls.Add(banner);
                    }
                }
                btn_send_mail.Enabled = true;
                email_panel.Controls.Remove(picture);
            }
            else
            {
                MessageBox.Show("Unable to connect to server! Please try again");

            }


        }

        private String sendEmail()
        {
            try
            {
                MailSender mailSender = new MailSender();
                mailSender.SendEmail(this.txt_email.Text, this.otp);
                return "success";
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return "faliure";
            }
        }

        private Boolean validatePassword()
        {
            if (txt_password.Text.Trim().Equals(txt_confirmpasword.Text.Trim()) && !txt_password.Text.Trim().Equals("")
                && !txt_confirmpasword.Text.Trim().Equals(""))
            {
                PictureBox pictureBox = Controls.Find("ptx_" + txt_confirmpasword.Name, true).FirstOrDefault() as PictureBox;
                Controls.Remove(pictureBox);
                return true;
            }
            else
            {
                PictureBox error = uiMessageUtitlity.AddPasswordErrorIcon(txt_confirmpasword.Name, txt_confirmpasword.Location.X + 255, txt_confirmpasword.Location.Y + 2);
                this.Controls.Add(error);
                MessageBox.Show("Passwords do not match!");
                return false;
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            if (this.validatePassword())
            {
                string password = PasswordHasher.CreatePasswordHash(txt_password.Text.Trim());
                UserCredential usersCredential = new UserCredential();
                using (DatabaseModel db = new DatabaseModel())
                {
                    usersCredential = db.Userscredentials.Find(this.txt_email.Text.Trim().ToLower());
                    usersCredential.Password = password;
                    db.SaveChanges();
                }

                MessageBox.Show("Password Resetted Successfully");

                if (window.Equals("Login"))
                {
                    Application.UserAppDataRegistry.SetValue("remeberMe", false);
                    Application.UserAppDataRegistry.SetValue("username", "");
                    Application.UserAppDataRegistry.SetValue("password", "");
                    Login login = new Login();
                    login.Show();
                    this.Close();
                }
                else
                {
                    Application.UserAppDataRegistry.SetValue("password", password);
                    this.Close();
                }

            }



        }

        private void txt_proceed_Click(object sender, EventArgs e)
        {
            if (this.txt_otp.Text.Trim().Equals(this.otp))
            {
                this.reset_panel.BringToFront();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (window.Equals("Login"))
            {
                if (Application.OpenForms.Count == 1)
                {
                    Application.Exit();
                }
            }
            else
            {
                if (Application.OpenForms.Count == 1)
                {
                    this.Close();
                }
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
