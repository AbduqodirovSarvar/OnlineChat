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
            builder.HasMany(x => x.Photos).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Messages).WithOne(x => x.Sender).HasForeignKey(x => x.SenderId);
            builder.HasData(new User()
            {
                FirstName = "SuperAdmin",
                LastName = "SuperAdmin",
                PasswordHash = hashService.GetHash("Sarvar.12345"),
                Email = "abduqodirovsarvar.2002@gmail.com",
                Role = Domain.Enums.UserRole.SuperAdmin
            });
        }
    }
}
