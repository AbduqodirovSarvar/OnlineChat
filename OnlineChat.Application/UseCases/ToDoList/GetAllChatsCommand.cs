using MediatR;
using OnlineChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class GetAllChatsCommand : IRequest<List<Chat>>
    {
        public GetAllChatsCommand() { }
    }
}
