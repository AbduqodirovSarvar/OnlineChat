using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class CreateNewMessageCommand : IRequest<bool>
    {
        public CreateNewMessageCommand() { }
        public CreateNewMessageCommand(Guid toUserId, Guid fromUserId, string msg)
        {
            ToUserId = toUserId;
            FromUserId = fromUserId;
            Msg = msg;
        }
        public Guid ToUserId { get; set; }
        public Guid FromUserId { get; set; }
        public string Msg { get; set; } = string.Empty;
    }
}
