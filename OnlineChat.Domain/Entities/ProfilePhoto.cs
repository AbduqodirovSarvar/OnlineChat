using OnlineChat.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Domain.Entities
{
    public class ProfilePhoto : AudiTable
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string PhotoName { get; set; } = null!;
    }
}
