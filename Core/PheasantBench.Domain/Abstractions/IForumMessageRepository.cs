using PheasantBench.Domain.Models;

namespace PheasantBench.Domain.Abstractions
{
    public interface IForumMessageRepository : IRepository<ForumMessage>
    {
        Task<int> GetMessageCountByThread(Guid threadId, int size);
        Task<IEnumerable<ForumMessage>> GetPagedByThreadAsync(int page, int size, Guid threadId, bool trackChanges);
    }
}
