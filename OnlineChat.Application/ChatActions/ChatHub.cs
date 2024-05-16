using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Abstractions;
using OnlineChat.Domain.Entities;
using OnlineChat.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.ChatActions
{
    [Authorize]
    public class ChatHub(
        ICurrentUserService currentUserService,
        IAppDbContext appDbContext
        ) : Hub
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IAppDbContext _context = appDbContext;

        public async Task JoinRoom(Guid roomId)
        {
            await Groups.AddToGroupAsync(_currentUserService.UserId.ToString(), roomId.ToString());
            await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", $"User joined with identifier {_currentUserService.UserId}");
            return;
        }

        public async Task SendMessage(Message message)
        {
            try
            {
                await Groups.AddToGroupAsync(_currentUserService.UserId.ToString(), message.Id.ToString());
                Console.WriteLine($"User joined to this room which identifier is {message.Id}");
            }
            catch 
            {
                Console.WriteLine($"User already joined to this room which identifier is {message.Id}");
            }
            
            await Clients.Group(message.Id.ToString()).SendAsync("ReceiveMessage", message);
            return;
        }

        public async Task SendConnectionUser(Guid roomId)
        {
            var user = await _context.Users
                                     .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId)
                                     ?? throw new NotFoundException("User");

            await Clients.Group(roomId.ToString()).SendAsync("ConnectedUser", user);
            return;
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await _context.Users
                                     .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId);
            if(user == null)
            {
                await base.OnDisconnectedAsync(exception);
                return;
            }

            await base.OnDisconnectedAsync(exception);
            return;
        }
    }
}
