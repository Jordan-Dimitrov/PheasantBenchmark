using FakeItEasy;
using PheasantBench.Application.Dtos;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;
using PheasantBench.Infrastructure.Services;

namespace PheasantBench.Tests.Services
{
    public class ForumThreadServiceTests
    {
        private readonly IForumThreadRepository _ForumThreadRepository;
        public ForumThreadServiceTests()
        {
            _ForumThreadRepository = A.Fake<IForumThreadRepository>();
        }

        [Test]
        public async Task ForumThreadService_CreateForumThread_ReturnsResponseOk()
        {
            var thread = new ForumThread { Id = Guid.NewGuid(), Name = "faked", Description = "Test" };
            var dto = new CreateForumThreadDto { Name = "faked", Description = "Test" };

            A.CallTo(() => _ForumThreadRepository.ExistsAsync(x => x.Name == "faked")).Returns(false);
            A.CallTo(() => _ForumThreadRepository.InsertAsync(thread)).Returns(true);

            var service = new ForumThreadService(_ForumThreadRepository);

            var result = await service.CreateForumThread(dto);

            Assert.AreEqual(result.Success, false);
        }

        [Test]
        public async Task ForumThreadService_CreateForumMessage_ReturnsResponseError()
        {
            ForumThread thread = new ForumThread() { Id = Guid.NewGuid(), Name = "faked", Description = "Test" };
            CreateForumThreadDto dto = new CreateForumThreadDto() { Name = "faked", Description = "Test" };

            A.CallTo(() => _ForumThreadRepository.ExistsAsync(x => x.Name == "")).Returns(true);
            A.CallTo(() => _ForumThreadRepository.InsertAsync(thread)).Returns(true);

            var service = new ForumThreadService(_ForumThreadRepository);
            var result = await service.CreateForumThread(dto);

            Assert.AreEqual(result.Success, false);
        }
    }
}
