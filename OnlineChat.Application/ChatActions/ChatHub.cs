using Microsoft.AspNetCore.SignalR;
using OnlineChat.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineChat.Application.ChatActions
{
    // [Authorize] // Uncomment this line if you want to require authorization
    public class ChatHub(
/*        IAppDbContext dbContext,
        ICurrentUserService currentUserService*/
        ) : Hub
    {
/*        private readonly IAppDbContext _context = dbContext;
        private readonly ICurrentUserService _currentUserService = currentUserService;*/
        private readonly static Dictionary<string, User> _users = new Dictionary<string, User>
        {
                { "1", new User("Alice") },
                { "2", new User("Bob") },
                { "3", new User("Charlie") }
        };

        public async Task SendMessage(string toUserId, string fromUserId, string message)
        {
            Console.WriteLine($"Message: {message}\nFrom: {fromUserId}\nTo: {toUserId}");
            await Clients.User(toUserId).SendAsync("ReceiveMessage", fromUserId, message);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var id = this.Context.ConnectionId;
            if (!_users.TryGetValue(id, out User? user))
                user = User.Unknown();

            _users.Remove(id);
            var msg = "A user has left the chat";
            await Clients.Others.SendAsync("ReceiveMessage", msg);
            await Clients.Others.SendAsync("Disconnected", user);
        }
    }

    public class User(string name)
    {
        public string Name { get; set; } = name;
        public static User Unknown() => new("[unknown]");
    }
}
