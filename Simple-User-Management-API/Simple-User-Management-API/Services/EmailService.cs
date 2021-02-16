using Simple_User_Management_API.Interfaces;
using Simple_User_Management_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Services
{
    public class EmailService: IEmailService
    {
        private EmailConfig _emailConfig;
        private const string HOST = "smtp.gmail.com";
        public EmailService(EmailConfig emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public void Send(string ToEmail, string Subject, string Body)
        {

            MailAddress fromAddress = new MailAddress(_emailConfig.Email);
            var toAddress = new MailAddress(ToEmail);
            string fromPassword = _emailConfig.Password;
            string subject = Subject;
            string body = Body;

            var smtp = new SmtpClient
            {
                Host = HOST,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
