using OnlineChat.Domain.Abstractions;
using OnlineChat.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Domain.Entities
{
    public class Message : AudiTable
    {
        public Guid SenderId { get; set; }
        public User? Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public User? Receiver { get; set; }
        public string Msg {  get; set; } = null!;
        public bool IsSeen { get; set; } = false;
        public DateTime SeenAt { get; set; }
    }
}
