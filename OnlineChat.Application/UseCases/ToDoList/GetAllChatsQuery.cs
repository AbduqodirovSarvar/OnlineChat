﻿using MediatR;
using OnlineChat.Application.Models;
using OnlineChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.ToDoList
{
    public class GetAllChatsQuery : IRequest<List<UserViewModel>>
    {
        public GetAllChatsQuery() { }
    }
}
