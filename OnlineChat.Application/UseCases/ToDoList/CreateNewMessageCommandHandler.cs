using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Abstractions;
using OnlineChat.Domain.Entities;
using OnlineChat.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class CreateNewMessageCommandHandler(
        IAppDbContext appDbContext,
        IEncryptionService encryptionService
        ) : IRequestHandler<CreateNewMessageCommand, bool>
    {
        private readonly IAppDbContext _context = appDbContext;
        private readonly IEncryptionService _encryptionService = encryptionService;
        public async Task<bool> Handle(CreateNewMessageCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users
                                            .FirstOrDefaultAsync(x => x.Id == request.FromUserId, cancellationToken)
                                            ?? throw new NotFoundException("Current User not found");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.ToUserId, cancellationToken)
                                           ?? throw new NotFoundException("User Not Found");

            var (encryptedContent, IV) = _encryptionService.Encrypt(request.Msg);

            var message = new Message()
            {
                SenderId = currentUser.Id,
                ReceiverId = user.Id,
                EncryptedContent = encryptedContent,
                IV = IV
            };

            await _context.Messages.AddAsync(message, cancellationToken);
            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}
