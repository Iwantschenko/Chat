using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public interface IRepository<Entity> where Entity : class
    {
        public Task<Entity> GetByID(Guid ID);
        public Task<List<Entity>> GetAll();
        public Task Add(Entity entity);
        public Task AddRange(IEnumerable<Entity> entities);
        public void Update(Entity entity);
        public void RemoveEntity(Entity entity);
        public Task Delete(Guid ID);

    }
}
