using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineChat.Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class CreateUserPhotoCommand : IRequest<UserViewModel>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public IFormFile Photo { get; set; } = null!;
    }
}
