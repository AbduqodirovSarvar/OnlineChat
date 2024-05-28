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
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class GetAllChatsQueryHandler(
        IAppDbContext dbContext,
        ICurrentUserService currentUserService,
        IMapper mapper
        ) : IRequestHandler<GetAllChatsQuery, List<UserViewModel>>
    {
        private readonly IAppDbContext _context = dbContext;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;

        public async Task<List<UserViewModel>> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users
                                            .Include(x => x.ReceivedMessages)
                                            .Include(x => x.SentMessages)
                                            .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken)
                                            ?? throw new NotFoundException("Current User not found");

            var Ids = currentUser.ReceivedMessages.Select(x => x.SenderId)
                                                  .ToList()
                                                  .Concat(currentUser.SentMessages
                                                                     .Select(x => x.ReceiverId).ToList())
                                                  .ToList();
/*
            var receiveFromIds = await _context.Messages
                                                  .Where(x => x.ReceiverId == currentUser.Id)
                                                  .Select(x => x.SenderId)
                                                  .ToListAsync(cancellationToken);

            var sendToIds = await _context.Messages
                                                  .Where(x => x.SenderId == currentUser.Id)
                                                  .Select(x => x.ReceiverId)
                                                  .ToListAsync(cancellationToken);*/

            var users = await _context.Users
                                            .Include(x => x.SentMessages)
                                            .Include(x => x.ReceivedMessages)
                                            .Where(x => Ids.Contains(x.Id))
                                            .Distinct()
                                            .ToListAsync(cancellationToken);

            return _mapper.Map<List<UserViewModel>>(users);
        }
    }
}
