using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineChat.Application.Models;
using OnlineChat.Domain.Behaviours;
using OnlineChat.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class UpdateUserCommand : IRequest<UserViewModel>
    {
        [Required]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public IFormFile? Photo { get; set; }
    }
}
