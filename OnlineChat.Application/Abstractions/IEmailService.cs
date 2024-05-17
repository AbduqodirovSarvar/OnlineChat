using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Abstractions
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string email, string subject, string body);
        Task<bool> SendEmailConfirmForResetPassword(string email);
        Task<bool> SendEmailConfirm(string email);
        bool CheckEmailConfirmed(string email, string confirmationCode);
    }
}
