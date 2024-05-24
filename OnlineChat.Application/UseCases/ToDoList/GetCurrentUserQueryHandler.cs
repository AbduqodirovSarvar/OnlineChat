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
    public class GetCurrentUserQueryHandler(
        ICurrentUserService currentUserService,
        IMapper mapper,
        IAppDbContext dbContext
        ) : IRequestHandler<GetCurrentUserQuery, UserViewModel>
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;
        private readonly IAppDbContext _context = dbContext;
        public async Task<UserViewModel> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                                           .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken)
                                           ?? throw new NotFoundException();

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
