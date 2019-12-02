using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Utility
{
    public class MailSender
    {
        public void SendEmail(String to, String otp)
        {
            MailMessage mailMsg = new MailMessage();

            mailMsg.To.Add(new MailAddress(to));

            mailMsg.From = new MailAddress("sheen.ruwisha12@gmail");

            mailMsg.Subject = "Event Manager Password Reset Verification";
            string text = $"Your OTP is  : {otp}";
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));

            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("apikey", "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");
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);
        }

    }
}
