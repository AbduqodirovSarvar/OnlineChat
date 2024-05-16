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
    public class DeleteUserPhotoCommandHandler(
        IAppDbContext dbContext,
        ICurrentUserService currentUserService,
        IFileService fileService
        ) : IRequestHandler<DeleteUserPhotoCommand, bool>
    {
        private readonly IAppDbContext _context = dbContext;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IFileService _fileService = fileService;
        public async Task<bool> Handle(DeleteUserPhotoCommand request, CancellationToken cancellationToken)
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
            var user = await _context.Users.Include(x => x.Photos)
                                           .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                           ?? throw new NotFoundException();

            var photo = await _context.Photos.FirstOrDefaultAsync(x => x.PhotoName == request.Name, cancellationToken)
                                             ?? throw new NotFoundException("Photo was not found");

            await _fileService.RemoveFileAsync(photo.PhotoName);
            photo.IsDeleted = true;

            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}
