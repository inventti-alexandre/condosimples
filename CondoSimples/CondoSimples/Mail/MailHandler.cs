using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Mail
{
    public class MailHandler
    {
        public static void SendMail(string body, string mailTo, string subject)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(mailTo));  // replace with valid value 
            message.From = new MailAddress("rafael.rdgs@uol.com.br");  // replace with valid value
            message.Subject = subject;
            message.Body = string.Format(body);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "XXXXX",  // replace with valid value
                    Password = "XXXXX"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtps.uol.com.br";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }
    }
}
