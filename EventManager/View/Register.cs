using EventManager.DatabaseHelper;
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

            this.txt_name.AutoSize = false;
            this.txt_name.Size = new System.Drawing.Size(250, 30);

            this.txt_phone.AutoSize = false;
            this.txt_phone.Size = new System.Drawing.Size(250, 30);

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
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    if (!control.Name.Equals("txt_phone"))
                    {
                        if (String.IsNullOrEmpty(control.Text.Trim()))
                        {
                            PictureBox pictureBox = Controls.Find("ptx_" + control.Name, true).FirstOrDefault() as PictureBox;
                            if (pictureBox == null)
                            {
                                PictureBox error = uiMessageUtitlity.AddErrorIcon(control.Name, control.Location.X + 255, control.Location.Y + 2);
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
            if (txt_username.Text.Trim().Equals("") || txt_email.Text.Trim().Equals("") ||
               txt_name.Text.Trim().Equals("") || 
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
                    MessageBox.Show("Password should contain atleast 6 characters");
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
            if (UserHelper.UserExists(txt_email.Text.Trim()))
            {
                return true;
            }
            this.AddEmailError();
            MessageBox.Show("Email Already Exixts");
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

        private bool ValidatePhone()
        {
            if (fieldValidator.IsValidPhone(txt_phone.Text.Trim()))
            {
                return true;
            }
            this.AddPhoneError();
            return false;
        }

        private void AddPhoneError()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.AddPhoneError));
            }
            else
            {
                PictureBox error = uiMessageUtitlity.AddErrorIcon(txt_phone.Name, txt_phone.Location.X + 255, txt_phone.Location.Y + 2);
                this.Controls.Add(error);
            }
        }

        private bool DoValidations()
        {
            return this.ValidateFields() &&  this.ValidatePassword() && ValidatePhone() && this.ValidateEmail() && this.CheckExistingEmail();
        }

        private async void btn_register_Click(object sender, EventArgs e)
        {
            if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
            {
                String id = commonUtil.GenerateId("user");
                PictureBox picture = commonUtil.AddLoaderImage(this.btn_register.Location.X + 205, this.btn_register.Location.Y + 2);
                btn_register.Enabled = false;
                Controls.Add(picture);
                User user = new User()
                {
                    UserId = id,
                    Email = txt_email.Text.Trim(),
                    Username = txt_username.Text.Trim(),
                    Name = txt_name.Text.Trim(),
                    Phone = txt_phone.Text.Trim(),
                    Image = commonUtil.BitmapToBase64(cpb_userimage.Image)
                };

                UserCredential userCredential = new UserCredential()
                {
                    UserId = id,
                    Password = PasswordHasher.CreatePasswordHash(txt_password.Text.Trim()),
                    Email = txt_email.Text.Trim(),
                    Username = txt_username.Text.Trim()
                };
                bool task = await Task.Run(() => this.DoValidations());
                if (task)
                {
                    bool register = await Task.Run(() => UserHelper.AddUser(user, userCredential));
                    if (register)
                    {
                        Controls.Remove(picture);
                        MessageBox.Show("Registration Successful");
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
            else
            {
                MessageBox.Show("Unable to connect to server! Please try again");
            }



        }

        private void lbl_login_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
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


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
            base.OnFormClosing(e);
        }


        private void txt_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar))
            {
                if (!char.IsControl(e.KeyChar))
                {
                    if (e.KeyChar != 32)
                    {
                        MessageBox.Show("Name can only contain alphabetical charatcters");
                        e.Handled = true;
                    }
                }
            }
        }

        private void txt_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("Phone can only contain numeric charatcters");
                e.Handled = true;
            }
        }

        private void txt_username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar))
            {
                if (!char.IsControl(e.KeyChar))
                {
                    if (e.KeyChar != 32)
                    {
                        MessageBox.Show("Name can only contain alphabetical charatcters");
                        e.Handled = true;
                    }
                }
            }
        }
    }
}

