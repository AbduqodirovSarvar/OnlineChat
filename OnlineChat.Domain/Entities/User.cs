﻿using OnlineChat.Domain.Abstractions;
using OnlineChat.Domain.Behaviours;
using OnlineChat.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Domain.Entities
{
    public class User : AudiTable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [MailValidation]
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.User;
        public ICollection<ProfilePhoto> Photos { get; set; } = new HashSet<ProfilePhoto>();
        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
