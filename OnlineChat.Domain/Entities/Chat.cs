using OnlineChat.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Domain.Entities
{
    public class Chat : AudiTable
    {
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
