using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Infrastructure.DbContexts.Configurations
{
    public class ProfilePhotoTypeConfiguration : IEntityTypeConfiguration<ProfilePhoto>
    {
        public void Configure(EntityTypeBuilder<ProfilePhoto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(x => x.PhotoName).IsUnique();
        }
    }
}
