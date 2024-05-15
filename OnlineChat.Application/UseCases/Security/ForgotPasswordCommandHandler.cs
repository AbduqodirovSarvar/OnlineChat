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
    public class ForgotPasswordCommandHandler(IAppDbContext context, IEmailService emailService) : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IEmailService _emailService = emailService;
        private readonly IAppDbContext _context = context;

        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
                                           ?? throw new NotFoundException();

            return await _emailService.SendEmailConfirmed(request.Email);
        }
    }
}
