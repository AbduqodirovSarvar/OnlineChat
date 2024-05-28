using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class MarkAsSeenMessageCommand : IRequest<bool>
    {
        public MarkAsSeenMessageCommand(string id) 
        {
            if(Guid.TryParse(id, out Guid userId))
            {
                UserId = userId;
            }
        }
        public Guid UserId { get; set; }
    }
}
