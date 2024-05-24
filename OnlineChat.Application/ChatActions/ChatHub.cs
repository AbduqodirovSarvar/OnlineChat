using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineChat.Application.ChatActions
{
    // [Authorize] // Uncomment this line if you want to require authorization
    public class ChatHub : Hub
    {
        private readonly static Dictionary<string, User> _users = [];

        public async Task SendMessage(string message)
        {
            Console.WriteLine("Test message received");
            await Clients.User("test").SendAsync(message);
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task Register(User user)
        {
            var id = this.Context.ConnectionId;

            if (_users.ContainsKey(id))
                return;

            _users.Add(id, user);
            await Clients.Others.SendAsync("Connected", user);

            var msg = "A new user has joined the chat";
            await Clients.Others.SendAsync("ReceiveMessage", msg);
            await Clients.Others.SendAsync("Connected", user);
        }

        public IEnumerable<User> GetOnlineUsers()
        {
            return _users.Values;
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
