using Microsoft.EntityFrameworkCore;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;
using System.Linq.Expressions;

namespace PheasantBench.Infrastructure.Repositories
{
    public class BenchmarkRepository : IBenchmarkRepository
    {
        private readonly ApplicationDbContext _Context;
        public BenchmarkRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<bool> DeleteAsync(Benchmark value)
        {
            _Context.Remove(value);

            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Benchmark, bool>> condition)
        {
            return await _Context.Benchmarks.AnyAsync(condition);
        }

        public async Task<IEnumerable<Benchmark>> GetAllAsync(bool trackChanges)
        {
            var query = _Context.Benchmarks.Include(x => x.User);

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<Benchmark?> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = _Context.Benchmarks.Include(x => x.User).Where(x => x.Id == id);

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
        }

        public async Task<Benchmark?> GetByAsync(Expression<Func<Benchmark, bool>> condition)
        {
            return await _Context.Benchmarks.Include(x => x.User).FirstOrDefaultAsync(condition);
        }

        public async Task<bool> InsertAsync(Benchmark value)
        {
            await _Context.AddAsync(value);
            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Benchmark value)
        {
            _Context.Update(value);

            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Benchmark>> GetPagedAsync(bool trackChanges, int page, int size)
        {
            var query = _Context.Benchmarks.Include(x => x.User)
                .Skip((page - 1) * size)
                .Take(size)
                .OrderByDescending(x => x.Score);

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<int> GetPageCount(int size)
        {
            var count = (double)await _Context.Benchmarks.CountAsync() / size;

            return (int)Math.Ceiling(count);
        }

        public async Task<int> GetCount()
        {
            return await _Context.Benchmarks.CountAsync();
        }
    }
}
