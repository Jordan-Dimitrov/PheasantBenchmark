using PheasantBench.Application.Dtos;

namespace PheasantBench.Application.Abstractions
{
    public interface IForumMessageService
    {
        Task CreateBenchmark(CreateBenchmarkDto benchmark, string token);
        Task DeleteBenchmark(Guid id);
        Task GetBenchmark(Guid id);
        Task GetBenchmarksPaged(int page, int size);
    }
}
