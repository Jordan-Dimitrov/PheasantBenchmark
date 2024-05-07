using Microsoft.EntityFrameworkCore;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;
using System.Linq.Expressions;

namespace PheasantBench.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _Context;
        public UserRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<bool> DeleteAsync(User value)
        {
            _Context.Remove(value);

            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(Expression<Func<User, bool>> condition)
        {
            return await _Context.Users.AnyAsync(condition);
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool trackChanges)
        {
            var query = _Context.Users;

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<User?> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = _Context.Users.Where(x => x.Id == id.ToString());

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
        }

        public async Task<User?> GetByAsync(Expression<Func<User, bool>> condition)
        {
            return await _Context.Users.FirstOrDefaultAsync(condition);
        }

        public async Task<bool> InsertAsync(User value)
        {
            await _Context.AddAsync(value);
            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(User value)
        {
            _Context.Update(value);

            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<User>> GetPagedAsync(bool trackChanges, int page, int size)
        {
            var query = _Context.Users.Skip(page * size).Take(size);

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }
    }
}
