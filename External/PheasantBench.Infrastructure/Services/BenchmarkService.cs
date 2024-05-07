using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;

namespace PheasantBench.Infrastructure.Services
{
    public class BenchmarkService : IBenchmarkService
    {
        public Task CreateBenchmark(CreateBenchmarkDto benchmark, string token)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBenchmark(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task GetBenchmark(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task GetBenchmarksPaged(int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}
