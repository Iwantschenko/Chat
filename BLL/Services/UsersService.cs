using DAL.Infrastructure;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UsersService : BaseService<User>
    {
        protected readonly UserRepository _userRepository;
        public UsersService(UserRepository UserRepos) : base(UserRepos)
        {
            _userRepository = UserRepos;
        }
        public async Task<User?> GetId(string Id) => await _userRepository.GetbyID(Id);
        public async Task Delete(string Id ) => await _userRepository.Delete(Id);
        
    }
}
