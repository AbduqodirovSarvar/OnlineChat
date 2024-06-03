using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineChat.Application.Abstractions;
using OnlineChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Infrastructure.DbContexts.Configurations
{
    public class UserTypeConfiguration(IHashService hashService) : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasMany(x => x.SentMessages).WithOne(x => x.Sender).HasForeignKey(x => x.SenderId);
            builder.HasMany(x => x.ReceivedMessages).WithOne(x => x.Receiver).HasForeignKey(x => x.ReceiverId);
            builder.HasData(
                new User()
                {
                FirstName = "Super Admin",
                LastName = "Chat Project's super admin",
                PasswordHash = hashService.GetHash("Sarvar.12345"),
                Email = "abduqodirovsarvar.2002@gmail.com",
                Role = Domain.Enums.UserRole.SuperAdmin
                }
               /* new User()
                {
                    FirstName = "User1 firstname",
                    LastName = "User1 lastname",
                    PasswordHash = hashService.GetHash("Sarvar.12345"),
                    Email = "user1@gmail.com",
                    Role = Domain.Enums.UserRole.User
                },
                new User()
                {
                    FirstName = "User2 firstname",
                    LastName = "user2 lastname",
                    PasswordHash = hashService.GetHash("Sarvar.12345"),
                    Email = "user2@gmail.com",
                    Role = Domain.Enums.UserRole.User
                }*/
                );
        }
    }
}
