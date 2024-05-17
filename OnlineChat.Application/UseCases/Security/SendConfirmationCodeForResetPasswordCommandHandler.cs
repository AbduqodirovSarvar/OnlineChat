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
    public class SendConfirmationCodeForResetPasswordCommandHandler(IAppDbContext context, IEmailService emailService) : IRequestHandler<SendConfirmationCodeForResetPasswordCommand, bool>
    {
        private readonly IEmailService _emailService = emailService;
        private readonly IAppDbContext _context = context;

        public async Task<bool> Handle(SendConfirmationCodeForResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
                                           ?? throw new NotFoundException();

            return await _emailService.SendEmailConfirmForResetPassword(request.Email);
        }
    }
}
