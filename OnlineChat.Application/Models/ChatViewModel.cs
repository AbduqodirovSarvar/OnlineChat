using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Models
{
    public class ChatViewModel
    {
        public UserViewModel User { get; set; } = null!;
        public ICollection<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
    }
}
