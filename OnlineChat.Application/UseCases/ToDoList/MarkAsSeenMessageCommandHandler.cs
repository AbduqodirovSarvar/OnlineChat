using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Abstractions;
using OnlineChat.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class MarkAsSeenMessageCommandHandler(
        IAppDbContext appDbContext,
        ICurrentUserService currentUserService
        ) : IRequestHandler<MarkAsSeenMessageCommand, bool>
    {
        private readonly IAppDbContext _context = appDbContext;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        public async Task<bool> Handle(MarkAsSeenMessageCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users
                                            .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken)
                                            ?? throw new NotFoundException("Current User not found");

            var user = await _context.Users.
                                            FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
                                            ?? throw new NotFoundException("User not found");

            var messages = await _context.Messages.Where(x => x.SenderId == user.Id 
                                                           && x.ReceiverId == currentUser.Id
                                                           && !x.IsSeen)
                                                  .ToListAsync(cancellationToken);

            foreach(var msg in messages)
            {
                msg.IsSeen = true;
                msg.SeenAt = DateTime.UtcNow;
            }

            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}
