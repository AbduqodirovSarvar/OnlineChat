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
        public CreateNewMessageCommand(Guid toUserId, string msg)
        {
            ToUserId = toUserId;
            Msg = msg;
        }
        public Guid ToUserId { get; set; }
        public string Msg { get; set; } = string.Empty;
    }
}
