using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Abstractions;
using OnlineChat.Application.Models;
using OnlineChat.Domain.Entities;
using OnlineChat.Domain.Exceptions;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class CreateUserPhotoCommandHandler(
        IFileService fileService,
        IAppDbContext dbContext,
        IMapper mapper
        ) : IRequestHandler<CreateUserPhotoCommand, UserViewModel>
    {
        private readonly IAppDbContext _context = dbContext;
        private readonly IFileService _fileService = fileService;
        private readonly IMapper _mapper = mapper;

        public async Task<UserViewModel> Handle(CreateUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(x => x.Photos)
                                           .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                           ?? throw new NotFoundException();

            var newPhoto = new ProfilePhoto()
            {
                UserId = user.Id,
                PhotoName = await _fileService.SaveFileAsync(request.Photo)
                                              ?? throw new Exception("Could not save this photo")
            };

            await _context.Photos.AddAsync(newPhoto, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            user.Photos.Add(newPhoto);
            
            return _mapper.Map<UserViewModel>(request);
        }
    }
}
