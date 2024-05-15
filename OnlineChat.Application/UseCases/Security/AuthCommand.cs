using AutoMapper;
using MediatR;
using OnlineChat.Application.Models;
using OnlineChat.Domain.Behaviours;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.Security
{
    public class AuthCommand(string email, string password) : IRequest<LoginViewModel>
    {
        [Required]
        [MailValidation]
        public string Email { get; set; } = email;
        [Required]
        [PasswordValidation]
        public string Password { get; set; } = password;
    }
}
