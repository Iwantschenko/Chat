using BLL.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Linq;

namespace BLL.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatsService _chatsService;
        private readonly UsersService _usersService;
        private static readonly Dictionary<string, string> _userConnectionMap = new Dictionary<string, string>();

        public ChatHub(ChatsService chatsService, UsersService usersService)
        {
            _chatsService = chatsService;
            _usersService = usersService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var user = await _usersService.GetId(userId);
            _userConnectionMap[userId] = Context.ConnectionId;
            if (user == null)
            {
                await _usersService.Create(new User()
                {
                    Id = userId,
                    Name = Context.User.Identity.Name
                });
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            _userConnectionMap.Remove(userId);
            await base.OnDisconnectedAsync(exception);
        }
        private static string? GetConnection(string userId)
        {
            _userConnectionMap.TryGetValue(userId, out var connection);
            return connection;
        }

        public async Task CreateChat(string ChatName)
        {
            var creatorID = Context.UserIdentifier;
            var chat = new Chat()
            {
                Chat_ID = Guid.NewGuid(),
                Name = ChatName,
                Creator_Id = creatorID,
                Users = new List<User>(),
            };

            var Creator = await _usersService.GetId(creatorID);
            if (Creator != null)
            {
                chat.Users.Add(Creator);
            }
            else
            {
                throw new HubException("User not found.");
            }
            await _chatsService.Create(chat);
            await Groups.AddToGroupAsync(Context.ConnectionId, chat.Chat_ID.ToString());
            await Clients.Group(chat.Chat_ID.ToString()).SendAsync("UserAddedToChat", $"User {Creator.Name} create chat -> {chat.Chat_ID.ToString()} ");

        }
        public async Task AddToChat(string ChatName, List<string> UserIds)
        {
            var chat = await _chatsService.GetbyName(ChatName);
            if (chat == null)
            {
                throw new HubException("Chat not found.");
            }
            foreach (var userId in UserIds)
            {
                var user = await _usersService.GetId(userId);
                if (user != null)
                {
                    chat.Users.Add(user);
                    await Clients.Group(chat.Chat_ID.ToString()).SendAsync("UserAddedToChat", user.Name);
                }

            }
            _chatsService.Update(chat);

        }
        public async Task JoinChat(Guid chatID)
        {
            var userId = Context.UserIdentifier;
            if (await _chatsService.IsChatExist(chatID))
            {
                var user = await _usersService.GetId(userId);
                if (user != null)
                {

                    await _chatsService.AddUserToGroup(chatID, user);
                    await Groups.AddToGroupAsync(GetConnection(userId), chatID.ToString());
                    await Clients.Group(chatID.ToString()).SendAsync("UserAddedToChat", "You have been added to a new chat", user.Name);
                }
            }
            else
                await Clients.User(Context.ConnectionId).SendAsync("Receive", "This group doesn't exists");
        }

        public async Task LeaveChat(Guid chatID)
        {
            var userId = Context.UserIdentifier;
            var chat = await _chatsService.GetById(chatID);
            var user = await _usersService.GetId(userId);
            if (chat != null && user != null)
            {
                if (chat.Creator_Id == user.Id)
                {
                    await _chatsService.RemoveCascade(chatID);
                }
                else
                    await _chatsService.RemoveUserFromGroup(chatID, user);
                await Groups.AddToGroupAsync(GetConnection(userId), chatID.ToString());
                await Clients.Group(chatID.ToString()).SendAsync("UserAddedToChat", "You have been added to a new chat", user.Name);

            }
            else
                await Clients.User(Context.ConnectionId).SendAsync("Receive", "This group doesn't exists");
        }



        public async Task SendMessage(Guid ChatId, string Message)
        {
            var userId = Context.UserIdentifier;
            if (await _chatsService.IsUserInChat(userId))
            {
                await Clients.Group(ChatId.ToString()).SendAsync("ReceiveMessage", Context.User.Identity.Name, Message);
            }
        }

        public async Task DeleteChat(Guid chatId)
        {
            var userId = Context.UserIdentifier;
            var chat = await _chatsService.GetById(chatId);

            if (chat == null)
            {
                await Clients.User(GetConnection(userId)).SendAsync("Receive", "Chat not found");
                return;
            }

            if (await _chatsService.IsCreator(chatId, userId))
            {

                var userConnections = chat.Users
                    .Select(u => GetConnection(u.Id))
                    .Where(conn => conn != null)
                    .ToList();
                foreach (var connection in userConnections)
                {
                    await Clients.Client(connection).SendAsync("ChatClosed", chat.Name);
                    await Groups.RemoveFromGroupAsync(connection, chatId.ToString());
                }
                await _chatsService.RemoveCascade(chatId);
            }
            else
            {
                await Clients.User(GetConnection(userId)).SendAsync("Receive", "You do not have access to delete this group");
            }
        }

    }
}
