﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Abstractions;
using OnlineChat.Application.Models;
using OnlineChat.Domain.Entities;
using OnlineChat.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class GetAllChatsQueryHandler(
        IAppDbContext dbContext,
        ICurrentUserService currentUserService,
        IMapper mapper) : IRequestHandler<GetAllChatsQuery, List<UserViewModel>>
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
                                                  .Concat(currentUser.SentMessages.Select(x => x.ReceiverId))
                                                  .Distinct()
                                                  .ToList();

            var users = await _context.Users
                                      .Include(x => x.SentMessages)
                                      .Include(x => x.ReceivedMessages)
                                      .Where(x => Ids.Contains(x.Id))
                                      .Select(user => new
                                      {
                                          User = user,
                                          LastMessageDate = user.SentMessages
                                              .Select(msg => msg.CreatedAt)
                                              .Concat(user.ReceivedMessages.Select(msg => msg.CreatedAt))
                                              .OrderByDescending(date => date)
                                              .FirstOrDefault()
                                      })
                                      .OrderByDescending(x => x.LastMessageDate)
                                      .Select(x => x.User)
                                      .ToListAsync(cancellationToken);

            return _mapper.Map<List<UserViewModel>>(users);
        }
    }
}
