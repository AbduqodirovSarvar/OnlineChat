using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OnlineChat.Application.Abstractions;
using OnlineChat.Application.UseCases.ToDoList;
using OnlineChat.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineChat.Application.ChatActions
{
    [Authorize]
    public class ChatHub(IMediator mediator) : Hub
    {
        private readonly IMediator _mediator = mediator;

        public async Task SendMessage(string toUserId, string message)
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext == null)
            {
                throw new ArgumentException(nameof(httpContext));
            }

            var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid fromUserId))
            {
                await Clients.User(toUserId).SendAsync("ReceiveMessage", fromUserId, message);
                await _mediator.Send(new CreateNewMessageCommand(Guid.Parse(toUserId), fromUserId, message));
            }
            else
            {
                throw new NotFoundException("Current User not found");
            }
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            Console.WriteLine($"User connected: {userId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            Console.WriteLine($"User disconnected: {userId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
