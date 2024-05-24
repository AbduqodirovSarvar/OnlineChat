using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class DeleteUserPhotoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
