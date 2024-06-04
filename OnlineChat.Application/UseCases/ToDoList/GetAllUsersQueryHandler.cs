using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Abstractions;
using OnlineChat.Application.Models;
using OnlineChat.Domain.Entities;
using OnlineChat.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class GetAllUsersQueryHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUserService) : IRequestHandler<GetAllUsersQuery, List<UserViewModel>>
    {
        private readonly IAppDbContext _context = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users
                                            .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken)
                                            ?? throw new NotFoundException("Current User not found");

            IQueryable<User> query = _context.Users
                                             .Where(x => x.Id != currentUser.Id);

            if (!string.IsNullOrEmpty(request.Text))
            {
                var searchText = request.Text.ToLower();
                query = query.Where(x => x.FirstName.ToLower().Contains(searchText)
                                      || x.LastName.ToLower().Contains(searchText)
                                      || x.Email.ToLower().Contains(searchText));
            }

            var users = await query.ToListAsync(cancellationToken);

            return _mapper.Map<List<UserViewModel>>(users);
        }
    }
}
