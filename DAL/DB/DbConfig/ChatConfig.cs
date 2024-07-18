using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.DB.DbConfig
{
    public class ChatConfig : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(x => x.Chat_ID);
            builder.Property(x => x.Name)
                .IsUnicode()
                .IsRequired()
                .HasColumnName("Chat_Name");
            builder
                .HasMany(chats => chats.Users)
                .WithMany(users => users.chats);

        }
    }
}
