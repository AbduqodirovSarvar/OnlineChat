using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Abstractions;
using OnlineChat.Application.Exceptions;
using OnlineChat.Application.Models;
using OnlineChat.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class UpdateUserCommandHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUserService
        ) : IRequestHandler<UpdateUserCommand, UserViewModel>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IAppDbContext _context = dbContext;
        public async Task<UserViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken)
                                                  ?? throw new NotFoundException("Current User not found");

            if (!(currentUser.Id == request.Id
                || currentUser.Role == Domain.Enums.UserRole.Admin
                || currentUser.Role == Domain.Enums.UserRole.SuperAdmin))
            {
                throw new AccessDeniedException();
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                           ?? throw new NotFoundException();

            user.FirstName = request.FirstName ?? user.FirstName;
            user.LastName = request.LastName ?? user.LastName;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
