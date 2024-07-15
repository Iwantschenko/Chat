using DAL.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public class BaseRepository<Entity> : IRepository<Entity>
        where Entity : class
    {
        protected readonly ChatDbContext _dbContext;
        public BaseRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Entity entity)
        {
            await _dbContext.Set<Entity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Entity> entities)
        {
            await _dbContext.Set<Entity>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid ID)
        {
            var item = await _dbContext.Set<Entity>().FindAsync(ID);
            if (item != null)
            {
                _dbContext.Set<Entity>().Remove(item);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Entity>> GetAll()
        {
            return await _dbContext
                .Set<Entity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Entity> GetByID(Guid ID)
        {
            return await _dbContext
               .Set<Entity>()
               .FindAsync(ID);
        }

        public void RemoveEntity(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Entity entity)
        {

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

    }
}
