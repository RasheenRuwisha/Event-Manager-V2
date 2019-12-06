using EventManager.DatabaseHelper;
using EventManager.Utility;
using EventManager.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager
{
    public partial class Login : Form
    {

        UiMessageUtitlity uiMessageUtitlity = new UiMessageUtitlity();
        CommonUtil commonUtil = new CommonUtil();
        FieldValidator fieldValidator = new FieldValidator();
        UiMessageUtitlity uiMessage = new UiMessageUtitlity();
        UserHelper userHelper = new UserHelper();
        public Login()
        {
            InitializeComponent();

            this.pb_logo.Image = Properties.Resources.logo;
            this.pb_logo.AutoSize = false;
            this.pb_logo.Left = this.Width / 2 - this.pb_logo.Width / 2;

            this.txt_email.AutoSize = false;
            this.txt_email.Size = new System.Drawing.Size(250, 30);
            this.txt_email.Left = this.Width / 2 - this.txt_email.Width / 2;

            this.txt_password.AutoSize = false;
            this.txt_password.Size = new System.Drawing.Size(250, 30);
            this.txt_password.Left = this.Width / 2 - this.txt_password.Width / 2;

            this.btn_login.Left = this.Width / 2 - this.btn_login.Width / 2;
            this.lbl_email.Left = (this.Width / 2) - 3 - this.txt_email.Width / 2;
            this.lbl_password.Left = (this.Width / 2) - 3 - this.txt_email.Width / 2;
        }

        private void lbl_register_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        private void lbl_forgotpassword_Click(object sender, EventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.Show();
            this.Close();
        }

        private async void btn_login_Click(object sender, EventArgs e)
        {
            pnl_error.Controls.Clear();
            PictureBox picture = commonUtil.addLoaderImage(this.btn_login.Location.X + 205, this.btn_login.Location.Y + 2);
            btn_login.Enabled = false;
            Controls.Add(picture);
            bool task = await Task.Run(() => this.DoValidations());
            if (task)
            {
                bool login = await Task.Run(() => UserHelper.ValidateUser(txt_email.Text.Trim(), txt_password.Text.Trim()));
                if (login)
                {
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    this.pnl_error.Controls.Add(uiMessage.AddBanner("Username and passwords do not match", "error"));
                    btn_login.Enabled = true;
                    Controls.Remove(picture);
                }
            }
            else
            {
                btn_login.Enabled = true;
                Controls.Remove(picture);
            }
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
                            using (PictureBox error = uiMessageUtitlity.AddErrorIcon(contorl.Name, contorl.Location.X + 255, contorl.Location.Y + 2))
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
            if (txt_email.Text.Trim().Equals("") ||txt_password.Text.Trim().Equals(""))
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
            if (UserHelper.UserExists(txt_email.Text.Trim()))
            {
                return true;
            }
            this.AddUserNotFoundBanner();
            return false;
        }

        public void AddUserNotFoundBanner()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.AddUserNotFoundBanner));
            }
            else
            {
                this.pnl_error.Controls.Add(uiMessage.AddBanner("User does not exist", "error"));
                PictureBox error = uiMessageUtitlity.AddErrorIcon(txt_email.Name, txt_email.Location.X + 255, txt_email.Location.Y + 2);
                this.Controls.Add(error);
            }
        }

        private bool DoValidations()
        {
            return this.ValidateFields() && this.CheckExistingEmail();
        }
    }
}
