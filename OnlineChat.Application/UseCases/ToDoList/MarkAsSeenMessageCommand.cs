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
        public ICollection<Guid> Ids { get; set; } = new HashSet<Guid>();
    }
}
