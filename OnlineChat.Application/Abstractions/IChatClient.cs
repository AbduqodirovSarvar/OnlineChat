using OnlineChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Abstractions
{
    public interface IChatClient : IAsyncDisposable
    {
        event EventHandler<Message> OnMessageReceived;
        event EventHandler<User> OnConnected;
        event EventHandler<User> OnDisconnected;

        Task<IEnumerable<User>> GetOnlineUsersAsync();
        Task SendMessageAsync(string message);
        Task StartAsync(User user);
        Task StopAsync();
    }
}
