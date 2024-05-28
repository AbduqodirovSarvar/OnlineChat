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
        IAppDbContext appDbContext
        ) : IRequestHandler<CreateNewMessageCommand, bool>
    {
        private readonly IAppDbContext _context = appDbContext;
        public async Task<bool> Handle(CreateNewMessageCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users
                                            .FirstOrDefaultAsync(x => x.Id == request.FromUserId, cancellationToken)
                                            ?? throw new NotFoundException("Current User not found");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.ToUserId, cancellationToken)
                                           ?? throw new NotFoundException("User Not Found");

            var message = new Message()
            {
                ReceiverId = user.Id,
                SenderId = currentUser.Id,
                Msg = request.Msg
            };

            await _context.Messages.AddAsync(message, cancellationToken);
            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}
