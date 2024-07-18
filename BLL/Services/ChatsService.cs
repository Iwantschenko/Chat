using DAL.Infrastructure;
using Microsoft.Identity.Client;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ChatsService : BaseService<Chat>
    {
        protected readonly ChatRepository _chatRepository;
        public ChatsService(ChatRepository ChatRepos) : base(ChatRepos)
        {
            _chatRepository = ChatRepos;
        }
        public async Task<bool> IsUserInChat(string userId) => await _chatRepository.IsUserInChat(userId);
        public async Task<bool> IsChatExist(Guid ID) => await _chatRepository.IsChatExist(ID);
        public async Task<bool> IsCreator(Guid ChatId, string userId)
        {
            var chat = await _chatRepository.GetbyID(ChatId);
            return chat?.Creator_Id == userId;
        }

        public async Task<Chat?> GetById(Guid ID) => await _chatRepository.GetbyID(ID);
        public async Task<Chat?> GetWithUser(Guid ID) => await _chatRepository.GetWithUsers(ID);
        public async Task RemoveCascade(Guid ID) => await _chatRepository.RemoveCascade(ID);
        public async Task<Chat?> GetbyName(string Name) => await _chatRepository.GetbyName(Name);
        public async Task AddUserToGroup(Guid ChatId , User user)
        {
            if (await _chatRepository.IsUserInChat(user.Id)!)
            {
                var chat = await _chatRepository.GetWithUsers(ChatId);
                chat.Users.Add(user);
                _chatRepository.Update(chat);
            }
        }
        public async Task RemoveUserFromGroup(Guid ChatId, User user)
        {
            if (await _chatRepository.IsUserInChat(user.Id)!)
            {
                var chat = await _chatRepository.GetWithUsers(ChatId);
                 chat.Users.Remove(user);
                _chatRepository.Update(chat);
            }
        }
    }
}
