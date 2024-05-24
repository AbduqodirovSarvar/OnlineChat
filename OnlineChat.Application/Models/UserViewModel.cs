using OnlineChat.Domain.Behaviours;
using OnlineChat.Domain.Entities;
using OnlineChat.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [MailValidation]
        public string Email { get; set; } = null!;
        public EnumViewModel Role { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
    }
}
