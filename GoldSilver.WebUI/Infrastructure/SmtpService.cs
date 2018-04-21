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
            //string body = "<p>The following is a message from {0} ({1})</p><p>{2}</p>";
            //MailMessage message = new MailMessage();
            //message.To.Add(new MailAddress("sodomaok@gmail.com"));
            //message.Subject = "GoldSilver Contact Form Message";
            //message.Body = string.Format(body, contactForm.Name, contactForm.Email, contactForm.Message);
            //message.IsBodyHtml = true;

            //using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            //{
            //    //Fix for gmail
            //    smtpClient.Credentials = new NetworkCredential("sodomaok@gmail.com", "ok855967");
            //    smtpClient.EnableSsl = true;
            //    smtpClient.UseDefaultCredentials = false;
            //    smtpClient.Timeout = 20000;
            //    smtpClient.Send(message);
            //}
            var fromAddress = new MailAddress("sodomaok@gmail.com", "GoldSilve Web Site Contact form");
            var toAddress = new MailAddress("o.bogonis@gmail.com", "Me");
            const string fromPassword = "ok855967";
            const string subject = "GoldSilve Web Site Contact form";

            string body = string.Format("<p>The following is a message from {0} ({1})</p><p>{2}</p>", contactForm.Name, contactForm.Email, contactForm.Message);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }

        }
    }

}