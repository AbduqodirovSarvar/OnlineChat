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
    public class GetUserQueryHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUserService
        ) : IRequestHandler<GetUserQuery, UserViewModel?>
    {
        private readonly IAppDbContext _context = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<UserViewModel?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken)
                                                  ?? throw new NotFoundException("Current user not found");

            if(request.Id != null)
            {
                return _mapper.Map<UserViewModel>(await _context.Users
                                                                .Include(x => x.SentMessages)
                                                                .Include(x => x.ReceivedMessages)
                                                                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) 
                                                                ?? throw new NotFoundException());
            }

            if(request.Email != null)
            {
                return _mapper.Map<UserViewModel>(await _context.Users
                                                                .Include(x => x.SentMessages)
                                                                .Include(x => x.ReceivedMessages)
                                                                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
                                                                ?? throw new NotFoundException());
            }
            return null;
        }
    }
}
