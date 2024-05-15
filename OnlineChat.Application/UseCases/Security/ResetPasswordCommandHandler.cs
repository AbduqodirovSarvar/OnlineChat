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
    public class ResetPasswordCommandHandler(IEmailService emailService, IAppDbContext context, IHashService hashService) : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IEmailService _emailService = emailService;
        private readonly IAppDbContext _context = context;
        private readonly IHashService _hashService = hashService;

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken) ?? throw new NotFoundException();

            if (_emailService.CheckEmailConfirmed(user.Email, request.ConfirmationCode.ToString()) && request.NewPassword == request.ConfirmNewPassword)
            {
                user.PasswordHash = _hashService.GetHash(request.NewPassword);
                return (await _context.SaveChangesAsync(cancellationToken)) > 0;
            }
            return false;
        }
    }
}
