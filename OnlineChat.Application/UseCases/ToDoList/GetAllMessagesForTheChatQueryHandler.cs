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
    public class GetAllMessagesForTheChatQueryHandler(
        IAppDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUserService
        ) : IRequestHandler<GetAllMessagesForTheChatQuery, UserViewModel>
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;
        private readonly IAppDbContext _context = dbContext;
        public async Task<UserViewModel> Handle(GetAllMessagesForTheChatQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users
                                            .Include(x => x.SentMessages)
                                            .Include(x => x.ReceivedMessages)
                                            .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken)
                                            ?? throw new NotFoundException();

            var user = await _context.Users
                                           .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
                                           ?? throw new NotFoundException();

            var messages = await _context.Messages
                                         .Where(x => (x.SenderId == currentUser.Id && x.ReceiverId == user.Id) || (x.SenderId == user.Id && x.ReceiverId == currentUser.Id))
                                         .ToListAsync(cancellationToken);

            /*return new ChatViewModel()
            {
                User = _mapper.Map<UserViewModel>(user),
                Messages = _mapper.Map<List<MessageViewModel>>(messages)
            };*/
            user.SentMessages = messages;
            user.ReceivedMessages = [];
            
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
