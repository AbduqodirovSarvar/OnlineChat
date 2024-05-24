using Microsoft.EntityFrameworkCore;
using OnlineChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Abstractions
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Message> Messages { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
