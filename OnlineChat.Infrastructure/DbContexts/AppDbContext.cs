using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineChat.Application.Abstractions;
using OnlineChat.Domain.Entities;
using OnlineChat.Infrastructure.DbContexts.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Infrastructure.DbContexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IServiceProvider serviceProvider) : DbContext(options), IAppDbContext
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var hashService = _serviceProvider.GetService<IHashService>();
            if(hashService != null )
            {
                modelBuilder.ApplyConfiguration(new UserTypeConfiguration(hashService));
            }
            else
            {
                Console.WriteLine("HashService is not registered in the service provider!");
            }
            modelBuilder.ApplyConfiguration(new MessageTypeConfiguration());
        }
    }
}
