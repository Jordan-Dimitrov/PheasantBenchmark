using Microsoft.EntityFrameworkCore;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;
using System.Linq.Expressions;

namespace PheasantBench.Infrastructure.Repositories
{
    public class UserUpvoteRepository : IUserUpvoteRepository
    {
        private readonly ApplicationDbContext _Context;
        public UserUpvoteRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<bool> DeleteAsync(UserUpvotes value)
        {
            _Context.Remove(value);
            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(Expression<Func<UserUpvotes, bool>> condition)
        {
            return await _Context.UsersUpvotes.AnyAsync(condition);
        }

        public async Task<IEnumerable<UserUpvotes>> GetAllAsync(bool trackChanges)
        {
            return await _Context.UsersUpvotes.ToListAsync();
        }

        public async Task<UserUpvotes?> GetByAsync(Expression<Func<UserUpvotes, bool>> condition)
        {
            return await _Context.UsersUpvotes.FirstOrDefaultAsync(condition);
        }

        public async Task<UserUpvotes?> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = _Context.UsersUpvotes.Where(x => x.ForumMessageId == id);

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
        }

        public async Task<IEnumerable<UserUpvotes>> GetPagedAsync(bool trackChanges, int page, int size)
        {
            var query = _Context.UsersUpvotes.Skip((page - 1) * size)
                .Take(size);

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<UserUpvotes?> GetUserUpvoteByMessageAndUserAsync(string userId, Guid forumId)
        {
            return await _Context.UsersUpvotes.Include(x => x.User).Include(x => x.ForumMessage)
                .FirstOrDefaultAsync(x => x.ForumMessageId == forumId && x.UserId == userId);
        }

        public async Task<bool> InsertAsync(UserUpvotes value)
        {
            await _Context.AddAsync(value);

            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(UserUpvotes value)
        {
            _Context.Update(value);

            return await _Context.SaveChangesAsync() > 0;
        }
    }
}
