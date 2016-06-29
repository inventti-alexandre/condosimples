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
            message.From = new MailAddress("condosimples@outlook.com.br");  // replace with valid value
            message.Subject = subject;
            message.Body = string.Format(body);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "condosimples@outlook.com.br",  // replace with valid value
                    Password = "atireiopaunogato666"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }

        public static void SendMailToUs(string body, string mailFrom, string subject)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress("condosimples@outlook.com.br"));  // replace with valid value 
            message.From = new MailAddress(mailFrom);  // replace with valid value
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "condosimples@outlook.com.br",  // replace with valid value
                    Password = "atireiopaunogato666"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }
    }
}
