﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace EventManager.Utility
{
    public class FieldValidator
    {
        public  bool IsValidEmailAddress(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        public bool IsValidPhone(string phone)
        {
            if(phone.Length > 0)
            {
                if (phone.Length < 10)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
