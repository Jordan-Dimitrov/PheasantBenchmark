using Microsoft.EntityFrameworkCore;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;
using System.Linq.Expressions;

namespace PheasantBench.Infrastructure.Repositories
{
    public class ForumMessageRepository : IForumMessageRepository
    {
        private readonly ApplicationDbContext _Context;
        public ForumMessageRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<bool> DeleteAsync(ForumMessage value)
        {
            _Context.Remove(value);

            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(Expression<Func<ForumMessage, bool>> condition)
        {
            return await _Context.ForumMessages.AnyAsync(condition);
        }

        public async Task<IEnumerable<ForumMessage>> GetAllAsync(bool trackChanges)
        {
            var query = _Context.ForumMessages;

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<ForumMessage?> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = _Context.ForumMessages.Where(x => x.Id == id);

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
        }

        public async Task<ForumMessage?> GetByAsync(Expression<Func<ForumMessage, bool>> condition)
        {
            return await _Context.ForumMessages.FirstOrDefaultAsync(condition);
        }

        public async Task<bool> InsertAsync(ForumMessage value)
        {
            await _Context.AddAsync(value);
            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ForumMessage value)
        {
            _Context.Update(value);

            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ForumMessage>> GetPagedAsync(bool trackChanges, int page, int size)
        {
            var query = _Context.ForumMessages.Skip((page - 1) * size)
                .Take(size);

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<IEnumerable<ForumMessage>> GetPagedByThreadAsync(int page, int size, Guid threadId, bool trackChanges)
        {
            var query = _Context.ForumMessages.Skip((page - 1) * size)
                .Take(size)
                .Where(x => x.ForumThreadId == threadId)
                .OrderByDescending(x => x.UpvoteCount);

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<int> GetPageCount(int size)
        {
            return Math.Max(await _Context.ForumMessages.CountAsync() / size, 1);
        }
    }
}
