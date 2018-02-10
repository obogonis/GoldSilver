using GoldSilver.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace GoldSilver.WebUI.Infrastructure
{
    public class SmtpService
    {
        public static void SendContactForm(ContactFormViewModel contactForm)
        {
            string body = "<p>The following is a message from {0} ({1})</p><p>{2}</p>";
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress("o.bogonis@gmail.com"));
            message.Subject = "GoldSilver Contact Form Message";
            message.Body = string.Format(body, contactForm.Name, contactForm.Email, contactForm.Message);
            message.IsBodyHtml = true;

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                //Fix for gmail
                smtpClient.Credentials = new NetworkCredential("sodoma91@gmail.com", "s0d0maandriy");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Timeout = 20000;
                smtpClient.Send(message);
            }
        }
    }

}