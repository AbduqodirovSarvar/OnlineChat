using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Models
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public UserViewModel? Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public UserViewModel? Receiver { get; set; }
        public string Msg { get; set; } = null!;
        public bool IsSeen { get; set; }
        public DateTime SeenAt { get; set; }
    }
}
