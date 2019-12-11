using EventManager.DatabaseHelper;
using EventManager.Model;
using EventManager.Utility;
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
    public partial class UserProfile : Form
    {
        readonly string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
        User user = new User();
        UserCredential userCredential = new UserCredential();

        CommonUtil commonUtil = new CommonUtil();
        public UserProfile()
        {
            InitializeComponent();
        }

        private async void UserProfile_Load(object sender, EventArgs e)
        {


            this.cpb_image.AutoSize = false;
            this.cpb_image.Left = this.Width / 2 - this.cpb_image.Width / 2;

            this.txt_name.AutoSize = false;
            this.txt_name.Size = new System.Drawing.Size(250, 30);

            this.txt_phone.AutoSize = false;
            this.txt_phone.Size = new System.Drawing.Size(250, 30);

            this.txt_username.AutoSize = false;
            this.txt_username.Size = new System.Drawing.Size(250, 30);

            this.txt_email.AutoSize = false;
            this.txt_email.Size = new System.Drawing.Size(250, 30);

            user = await Task.Run(() => UserHelper.GetUser(userId));

            cpb_image.Image = commonUtil.Base64ToBitmap(user.Image);
            txt_name.Text = user.Name;
            txt_username.Text = user.Username;
            txt_email.Text = user.Email;
            txt_phone.Text = user.Phone;



        }

        private void lbl_forgotpassword_Click(object sender, EventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword("User");
            forgotPassword.ShowDialog();
        }

        private async void btn_update_Click(object sender, EventArgs e)
        {

            user.Name = txt_name.Text.Trim();
            user.Email = txt_email.Text.Trim();
            user.Phone = txt_phone.Text.Trim();
            user.Username = txt_username.Text.Trim();
            user.Image = commonUtil.BitmapToBase64(cpb_image.Image);

            Application.UserAppDataRegistry.SetValue("image", commonUtil.BitmapToBase64(cpb_image.Image));

            bool updated = await Task.Run(() => UserHelper.UpdateUser(user));
            if (updated)
            {
                commonUtil.AddUserUpdatedDetailsToLocalApp(user);
                MessageBox.Show("Profile Details Updated Successfully");
            }
            else
            {
                MessageBox.Show("Profile Details Not Updated. Please Try Again Later");
            }
        }

        private void pb_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cpb_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png",

            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(openFileDialog.FileName);
                long fileSize = fi.Length;
                if (fileSize > 1000000)
                {
                    MessageBox.Show("File to large");
                }
                else
                {

                    Bitmap bitmap = new Bitmap(openFileDialog.FileName);
                    cpb_image.Image = bitmap;
                }
            }
        }

        private void txt_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar))
            {
                if (!char.IsControl(e.KeyChar))
                {
                    if (e.KeyChar != 32)
                    {
                        MessageBox.Show("Name can only contain alphabetical characters");
                        e.Handled = true;
                    }
                }
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
                        MessageBox.Show("Name can only contain alphabetical characters");
                        e.Handled = true;
                    }
                }
            }
        }

        private void txt_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("Phone can only contain numeric characters");
                e.Handled = true;
            }
        }
    }
}
