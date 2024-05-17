using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.Security
{
    public class SendConfirmationCodeCommand : IRequest<bool>
    {
        [Required]
        public string Email { get; set; } = null!;
    }
}
