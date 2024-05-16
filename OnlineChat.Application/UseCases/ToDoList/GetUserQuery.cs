using MediatR;
using OnlineChat.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class GetUserQuery : IRequest<UserViewModel?>
    {
        public Guid? Id { get; set; }
        public string? Email { get; set; }
    }
}
