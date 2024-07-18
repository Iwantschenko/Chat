using DAL.DB;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
namespace DAL.Infrastructure
{
    public class UserRepository : BaseRepository<User>
    {
        private readonly ChatDbContext _context;
        public UserRepository(ChatDbContext context):base (context)
        {
            _context = context;
        }
        public async Task<User?> GetbyID(string ID)
        {
            return await _context.Users
                .FindAsync(ID);
        }
        public async Task Delete(string ID)
        {
            var user = await GetbyID(ID);
            if (user != null)
            {
                 _context.Users.Remove(user);
                 await _context.SaveChangesAsync();
            }
        
        }
        public async Task<List<User>> GetUsersInChatsAsync()
        {
            return await _context.Chats
                                 .SelectMany(c => c.Users)
                                 .Distinct()
                                 .ToListAsync();
        }
    }
}
