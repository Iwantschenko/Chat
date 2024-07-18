using DAL.DB;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Globalization;

namespace DAL.Infrastructure
{
    public class ChatRepository : BaseRepository<Chat>
    {
        private readonly ChatDbContext _context;
        public ChatRepository(ChatDbContext dbContext) :base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> IsChatExist(Guid ID)
        {
            var chat = await _context.Chats
                .FindAsync(ID);
            if (chat != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> IsUserInChat(string userId)
        {
            return await _context
                .Chats
                .AnyAsync(chat => chat.Users.Any(user => user.Id == userId));
        }
        
        public async Task<Chat?> GetbyID(Guid ID)
        {
            return await _context.Chats

                .FindAsync(ID);
        }

        public async Task<Chat?> GetWithUsers(Guid ID)
        {
            return await _context.Chats
                                 .Include(c => c.Users)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(c => c.Chat_ID == ID);
        }

        public async Task RemoveCascade(Guid ID)
        {
            var chat  = await _context.Chats
                                 .Include(c => c.Users)
                                 .ThenInclude(u => u.Id)
                                 .FirstOrDefaultAsync(c => c.Chat_ID == ID);
            if (chat != null)
            {
                _context.Chats.Remove(chat);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Chat?> GetbyName(string Name)
        {
            return await _context.Chats.FirstOrDefaultAsync(c => c.Name == Name);
        }
    }
}
