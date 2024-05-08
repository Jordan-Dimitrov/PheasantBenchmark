using PheasantBench.Domain.Models;

namespace PheasantBench.Domain.Abstractions
{
    public interface IUserUpvoteRepository : IRepository<UserUpvotes>
    {
        Task<UserUpvotes?> GetUserUpvoteByMessageAndUserAsync(string userId, Guid forumId);
    }
}
