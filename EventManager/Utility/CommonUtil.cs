﻿using EventManager.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.Utility
{
    public class CommonUtil
    {

        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

        public String generateOTP()
        {
            var stringChars1 = new char[6];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars[random1.Next(chars.Length)];
            }
            return new String(stringChars1);
        }

        public String generateUserId(string type)
        {
            var stringChars1 = new char[6];
            var random1 = new Random();
            string id = "";
            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars[random1.Next(chars.Length)];
            }
            if (type.Equals("user"))
            {
                id = $"USR_{new String(stringChars1)}";
                return id;
            }

            if (type.Equals("contact"))
            {
                id = $"CNT_{new String(stringChars1)}";
                return id;
            }

            if (type.Equals("event"))
            {
                id = $"ENT_{new String(stringChars1)}";
                return id;
            }
            return id;
        }

        public PictureBox addLoaderImage(int x, int y)
        {
            PictureBox picture = new PictureBox();
            picture.Image = Properties.Resources.loader;
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.Width = 25;
            picture.Height = 25;
            picture.Location = new Point(x, y);
            return picture;
        }

        public void removeSavedData()
        {
            Application.UserAppDataRegistry.DeleteValue("remeberMe");
            Application.UserAppDataRegistry.DeleteValue("userID");
            Application.UserAppDataRegistry.DeleteValue("username");
            Application.UserAppDataRegistry.DeleteValue("email");
            Application.UserAppDataRegistry.DeleteValue("password");

        }

        public String BitmapToBase64(Image image)
        {
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] byteString = memoryStream.GetBuffer();
            string base64 = Convert.ToBase64String(byteString);
            return base64;
        }

        public Bitmap Base64ToBitmap(String base64)
        {
            byte[] byteArray = Convert.FromBase64String(base64);
            MemoryStream memoryStream = new MemoryStream(byteArray);
            Bitmap bitmap = new Bitmap((Bitmap)Image.FromStream(memoryStream));
            return bitmap;
        }

        public void AddUserDetailsToLocalApp(UserCredential  user)
        {
            Application.UserAppDataRegistry.SetValue("userID", user.UserId);
            Application.UserAppDataRegistry.SetValue("username", user.Username);
            Application.UserAppDataRegistry.SetValue("email", user.Email);
            Application.UserAppDataRegistry.SetValue("password", user.Password);
            Application.UserAppDataRegistry.SetValue("remeberMe", true);
        }
    }
}
