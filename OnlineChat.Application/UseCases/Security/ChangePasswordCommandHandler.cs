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
    public class ChangePasswordCommandHandler(IAppDbContext context, IHashService hashService) 
        : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IAppDbContext _context = context;
        private readonly IHashService _hashService = hashService;

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
                                          ?? throw new NotFoundException();

            if (request.NewPassword == request.ConfirmPassword && _hashService.VerifyHash(request.OldPassword, user.PasswordHash))
            {
                user.PasswordHash = _hashService.GetHash(request.NewPassword);
                return (await _context.SaveChangesAsync(cancellationToken)) > 0;
            }
            return false;
        }
    }
}
