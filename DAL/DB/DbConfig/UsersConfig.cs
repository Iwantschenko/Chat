using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DB.DbConfig
{
    public class UsersConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("User_Id");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("User_Name");
            builder
                .HasMany(user => user.chats)
                .WithMany(chats => chats.Users);
        }
    }
}
