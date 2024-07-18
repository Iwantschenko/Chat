using Moq;
using BLL.Services;
using DAL.Infrastructure;
using Models.Entities;
using DAL.Migrations;
using DAL.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
namespace Test
{
    public class BaseServiceTest
    {
        private readonly MockRepositorySetup _mockSetup;
        private readonly BaseService<Chat> _service;

        public BaseServiceTest()
        {
            _mockSetup = new MockRepositorySetup();
            _service = new BaseService<Chat>(_mockSetup.MockRepository.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsAllChats()
        {
            var list = new List<Chat>
        {
            new Chat { Chat_ID = Guid.NewGuid(), Name = "Name1" },
            new Chat { Chat_ID = Guid.NewGuid(), Name = "Name2" },
            new Chat { Chat_ID = Guid.NewGuid(), Name = "Name3" }
        };
            _mockSetup.SetupMockGetAll(list);  
            var result = await _service.GetAll();

            Assert.Equal(3, result.Count);
            Assert.Equal("Name1", result[0].Name);
            Assert.Equal("Name2", result[1].Name);
            Assert.Equal("Name3", result[2].Name);
        }

        [Fact]
        public async Task Create_AddsChat()
        {
            
            var chat = new Chat { Chat_ID = Guid.NewGuid(), Name = "NewChat" };
            _mockSetup.SetupMockAdd();  
            await _service.Create(chat);

          
            _mockSetup.MockRepository.Verify(repo => repo.Add(chat), Times.Once);
        }

        [Fact]
        public async Task CreateRange_AddsMultipleChats()
        {
   
            var chats = new List<Chat>
        {
            new Chat { Chat_ID = Guid.NewGuid(), Name = "Chat1" },
            new Chat { Chat_ID = Guid.NewGuid(), Name = "Chat2" }
        };
            _mockSetup.SetupMockAddRange();
            await _service.CreateRange(chats);

           
            _mockSetup.MockRepository.Verify(repo => repo.AddRange(chats), Times.Once);
        }

        [Fact]
        public void Remove_DeletesChat()
        {
          
            var chat = new Chat { Chat_ID = Guid.NewGuid(), Name = "ChatToDelete" };
            _mockSetup.SetupMockRemove();
            _service.Remove(chat);

            _mockSetup.MockRepository.Verify(repo => repo.RemoveEntity(chat), Times.Once);
        }

        [Fact]
        public void Update_UpdatesChat()
        {
        
            var chat = new Chat { Chat_ID = Guid.NewGuid(), Name = "ChatToUpdate" };
            _mockSetup.SetupMockUpdate();
            _service.Update(chat);

            _mockSetup.MockRepository.Verify(repo => repo.Update(chat), Times.Once);
        }
    }
}