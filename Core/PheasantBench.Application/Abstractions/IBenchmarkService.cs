using PheasantBench.Application.Dtos;

namespace PheasantBench.Application.Abstractions
{
    public interface IBenchmarkService
    {
        Task<Response> CreateBenchmark(CreateBenchmarkDto benchmark, string token);
        Task<Response> DeleteBenchmark(Guid id);
        Task<DataResponse<BenchmarkDto>> GetBenchmark(Guid id);
        Task<DataResponse<ICollection<BenchmarkDto>>> GetBenchmarksPaged(int page, int size);
    }
}
