using OnlineChat.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Services
{
    public class EmailService : IEmailService
    {
        private static readonly Dictionary<string, string> ConfirmationCodes = [];

        private readonly string userName = "abduqodirovsarvar.2002@gmail.com";
        private readonly string appPassword = "hgjb mvzo nuji fcwb";

        public bool CheckEmailConfirmed(string email, string confirmationCode)
        {
            if (ConfirmationCodes[email] == confirmationCode.ToString())
            {
                ConfirmationCodes.Remove(email);
                return true;
            }
            return false;
        }

        public async Task<bool> SendEmail(string email, string subject, string body)
        {
            var model = new
            {
                Name = "Test Email",
                Email = email,
                Message = body,
                Subject = subject,
            };

            body = $"<strong>{model.Subject}:</strong> {model.Message}";

            var message = new MailMessage();
            message.To.Add(new MailAddress(model.Email));
            message.From = new MailAddress("abduqodirovsarvar.2002@gmail.com");
            message.Subject = model.Subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(userName, appPassword),
                EnableSsl = true,
            };

            try
            {
                await smtp.SendMailAsync(message);
                Console.WriteLine("Message sent!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendEmailConfirmForResetPassword(string email)
        {
            int confirmationCode = RandomNumberGenerator.GetInt32(10000, 99999);
            if (await SendEmail(email, "Confirmation code for reset password", confirmationCode.ToString()))
            {
                ConfirmationCodes.Remove(email);
                ConfirmationCodes.Add(email, confirmationCode.ToString());
                return true;
            }
            return false;
        }

        public async Task<bool> SendEmailConfirm(string email)
        {
            int confirmationCode = RandomNumberGenerator.GetInt32(10000, 99999);
            if (await SendEmail(email, "Confirma your email address", confirmationCode.ToString()))
            {
                ConfirmationCodes.Remove(email);
                ConfirmationCodes.Add(email, confirmationCode.ToString());
                return true;
            }
            return false;
        }
    }
}
