using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.DB.DbConfig
{
    public class ChatConfig : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("Chat_Id");
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Chat_Name");
            builder
                .HasMany(chats => chats.Users)
                .WithMany(users => users.chats);

        }
    }
}
