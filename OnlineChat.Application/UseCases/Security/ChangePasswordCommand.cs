using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.Security
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        public string OldPassword { get; set; } = default!;
        [Required]
        public string NewPassword { get; set; } = default!;
        [Required]
        public string ConfirmPassword { get; set; } = default!;
    }
}
