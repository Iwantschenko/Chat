using DAL.DB.DbConfig;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
namespace DAL.DB
{
    public class ChatDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) 
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfig());
            modelBuilder.ApplyConfiguration(new ChatConfig());
            
            base.OnModelCreating(modelBuilder);
            if (Database.IsRelational())
            {
                // Relational-specific configurations
            }
        }
    }
}
