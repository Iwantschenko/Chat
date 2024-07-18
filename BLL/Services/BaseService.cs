using DAL.Infrastructure;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public  class BaseService<Entity> where Entity : class
    {
        private readonly IRepository<Entity> _repository;
        public BaseService(IRepository<Entity> repos)
        {
            _repository = repos;
        }
        public virtual async Task<List<Entity>> GetAll() => await _repository.GetAll();
        public virtual async Task Create(Entity entity) => await _repository.Add(entity);
        public virtual async Task CreateRange(IEnumerable<Entity> entities) => await _repository.AddRange(entities);
        public virtual void Remove(Entity entity) => _repository.RemoveEntity(entity);
        public virtual void Update(Entity entity) => _repository.Update(entity);
    }
}
