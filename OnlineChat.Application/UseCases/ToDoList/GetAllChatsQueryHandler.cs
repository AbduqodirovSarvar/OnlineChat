using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Abstractions;
using OnlineChat.Domain.Entities;
using OnlineChat.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class GetAllChatsQueryHandler(IAppDbContext dbContext,
        ICurrentUserService currentUserService
        ) : IRequestHandler<GetAllChatsQuery, List<Chat>>
    {
        private readonly IAppDbContext _context = dbContext;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<List<Chat>> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users
                                            .Include(x => x)
                                            .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken)
                                            ?? throw new NotFoundException("Current User not found");


            throw new NotImplementedException();
        }
    }
}
