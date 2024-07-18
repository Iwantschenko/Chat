using DAL.Infrastructure;
using Models.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class MockRepositorySetup
    {
        private readonly Mock<IRepository<Chat>> _mockRepository;

        public MockRepositorySetup()
        {
            _mockRepository = new Mock<IRepository<Chat>>();
        }

        public Mock<IRepository<Chat>> MockRepository => _mockRepository;

        public void SetupMockGetAll(List<Chat> list)
        {
            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(list);
        }

        public void SetupMockAdd()
        {
            _mockRepository.Setup(repo => repo.Add(It.IsAny<Chat>())).Returns(Task.CompletedTask);
        }

        public void SetupMockAddRange()
        {
            _mockRepository.Setup(repo => repo.AddRange(It.IsAny<IEnumerable<Chat>>())).Returns(Task.CompletedTask);
        }

        public void SetupMockRemove()
        {
            _mockRepository.Setup(repo => repo.RemoveEntity(It.IsAny<Chat>()));
        }

        public void SetupMockUpdate()
        {
            _mockRepository.Setup(repo => repo.Update(It.IsAny<Chat>()));
        }
    }
}
