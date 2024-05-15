using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Models
{
    public sealed class LoginViewModel(UserViewModel user, string token)
    {
        public UserViewModel User { get; private set; } = user;
        public string AccessToken { get; private set; } = token;
    }
}
