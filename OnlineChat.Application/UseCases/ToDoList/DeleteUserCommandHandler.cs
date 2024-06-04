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
    public class DeleteUserCommandHandler(
        IAppDbContext dbContext,
        ICurrentUserService currentUser,
        IFileService fileService
        ) : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IAppDbContext _context = dbContext;
        private readonly ICurrentUserService _currentUserService = currentUser;
        private readonly IFileService _fileService = fileService;
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users
                                            .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken)
                                            ?? throw new NotFoundException("Current User not found");

            if (!(currentUser.Id == request.Id
                || currentUser.Role == Domain.Enums.UserRole.Admin
                || currentUser.Role == Domain.Enums.UserRole.SuperAdmin))
            {
                throw new Exception("Access denied");
            }

            var user = await _context.Users
                                        .Include(x => x.SentMessages)
                                        .Include(x => x.ReceivedMessages)
                                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                        ?? throw new NotFoundException();

            if(user.PhotoName != null)
            {
                await _fileService.RemoveFileAsync(user.PhotoName);
                user.PhotoName = null;
            }
            user.IsDeleted = true;
            foreach(var msg in user.SentMessages.Concat(user.ReceivedMessages))
            {
                msg.IsDeleted = true;
            }
            
            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}
