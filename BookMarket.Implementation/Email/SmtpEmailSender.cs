using BookMarket.Application.Email;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BookMarket.Implementation.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        //private readonly string _fromEmail;
        //private readonly string _fromPassword;

        //public SmtpEmailSender(string fromEmail, string fromPassword)
        //{
        //    _fromEmail = fromEmail;
        //    _fromPassword = fromPassword;
        //}

        public void Send(SendEmailDto dto)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("nemanja.srb1234@gmail.com", "******************")
            };

            var message = new MailMessage("nemanja.srb1234@gmail.com", dto.SendTo);
            message.Subject = dto.Subject;
            message.Body = dto.Content;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}
