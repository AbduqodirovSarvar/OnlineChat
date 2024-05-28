using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Application.Abstractions;
using OnlineChat.Application.Services;
using OnlineChat.Application.UseCases.ToDoList;
using OnlineChat.Domain.Entities;
using OnlineChat.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineChat.Application.ChatActions
{
    [Authorize]
    public class ChatHub(
        IMediator mediator,
        ICurrentUserService currentUserService
        ) : Hub
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task SendMessage(string toUserId, string message)
        {
            var httpContext = Context.GetHttpContext();
            if(httpContext == null )
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid fromUserId))
            {
                Console.WriteLine($"{fromUserId} {message}");
                Console.WriteLine($"Message: {message}\nTo: {toUserId}");
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
