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

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class GetAllUsersQueryHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUserService
        ) : IRequestHandler<GetAllUsersQuery, List<UserViewModel>>
    {
        private readonly IAppDbContext _context = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users
                                            .Where(x => !x.IsDeleted)
                                            .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken)
                                            ?? throw new NotFoundException("Current User not found");

            if(!(currentUser.Role == Domain.Enums.UserRole.Admin || currentUser.Role == Domain.Enums.UserRole.SuperAdmin))
            {
                throw new Exception("Access denied");
            }

            return _mapper.Map<List<UserViewModel>>(await _context.Users.ToListAsync(cancellationToken));
        }
    }
}
