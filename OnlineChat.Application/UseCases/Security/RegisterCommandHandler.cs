using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Abstractions;
using OnlineChat.Application.Models;
using OnlineChat.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.Security
{
    public class RegisterCommandHandler(IAppDbContext context, IMapper mapper, IHashService hashService, IFileService fileService) 
        : IRequestHandler<RegisterCommand, UserViewModel>
    {
        private readonly IAppDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IHashService _hashService = hashService;
        private readonly IFileService _fileService = fileService;

        public async Task<UserViewModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
            if (user != null)
            {
                throw new AlreadyExistsException($"user already exists with email: {user.Email}");
            }

            user = new Domain.Entities.User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = _hashService.GetHash(request.Password),
                Role = Domain.Enums.UserRole.User
            };

            /*if(request.Photo != null)
            {
                var photo = 
            }*/

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
