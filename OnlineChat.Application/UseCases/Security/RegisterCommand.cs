using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineChat.Application.Models;
using OnlineChat.Domain.Behaviours;
using OnlineChat.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.Security
{
    public class RegisterCommand : IRequest<UserViewModel>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [MailValidation]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public UserRole Role { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
