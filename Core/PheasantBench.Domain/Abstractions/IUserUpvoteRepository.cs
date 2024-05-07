using PheasantBench.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheasantBench.Domain.Abstractions
{
    public interface IUserUpvoteRepository : IRepository<UserUpvotes>
    {
        Task<UserUpvotes?> GetUserUpvoteByMessageAndUserAsync(string userId, Guid forumId);
    }
}
