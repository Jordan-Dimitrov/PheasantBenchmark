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
            var query = _Context.Benchmarks;

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public async Task<Benchmark?> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = _Context.Benchmarks.Where(x => x.Id == id);

            return await (trackChanges ? query.FirstOrDefaultAsync() : query.AsNoTracking().FirstOrDefaultAsync());
        }

        public async Task<Benchmark?> GetByAsync(Expression<Func<Benchmark, bool>> condition)
        {
            return await _Context.Benchmarks.FirstOrDefaultAsync(condition);
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
            var query = _Context.Benchmarks.Skip(page * size).Take(size);

            return await (trackChanges ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }
    }
}
