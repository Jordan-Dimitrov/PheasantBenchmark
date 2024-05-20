using Microsoft.EntityFrameworkCore;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;
using System.Linq.Expressions;

namespace PheasantBench.Infrastructure.Repositories
{
    public class ForumThreadRepository : IForumThreadRepository
    {
        private readonly ApplicationDbContext _Context;
        public ForumThreadRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<bool> DeleteAsync(ForumThread value)
        {
            _Context.Remove(value);

            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(Expression<Func<ForumThread, bool>> condition)
        {
            return await _Context.ForumThreads.AnyAsync(condition);
        }

        public async Task<IEnumerable<ForumThread>> GetAllAsync(bool trackChanges)
        {
            var query = _Context.ForumThreads;

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<ForumThread?> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = _Context.ForumThreads.Where(x => x.Id == id);

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
        }

        public async Task<ForumThread?> GetByAsync(Expression<Func<ForumThread, bool>> condition)
        {
            return await _Context.ForumThreads.FirstOrDefaultAsync(condition);
        }

        public async Task<bool> InsertAsync(ForumThread value)
        {
            await _Context.AddAsync(value);
            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ForumThread value)
        {
            _Context.Update(value);

            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ForumThread>> GetPagedAsync(bool trackChanges, int page, int size)
        {
            var query = _Context.ForumThreads.Skip((page - 1) * size)
                .Take(size);

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<int> GetPageCount(int size)
        {
            var count = (double)await _Context.ForumThreads.CountAsync() / size;

            return (int)Math.Ceiling(count);
        }
    }
}
