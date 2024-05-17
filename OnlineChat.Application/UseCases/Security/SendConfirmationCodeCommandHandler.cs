using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Abstractions;
using OnlineChat.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.Security
{
    public class SendConfirmationCodeCommandHandler(IEmailService emailService) : IRequestHandler<SendConfirmationCodeCommand, bool>
    {
        private readonly IEmailService _emailService = emailService;

        public async Task<bool> Handle(SendConfirmationCodeCommand request, CancellationToken cancellationToken)
        {
            return await _emailService.SendEmailConfirm(request.Email);
        }
    }
}
