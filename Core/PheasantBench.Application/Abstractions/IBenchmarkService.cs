using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;

namespace PheasantBench.Application.Abstractions
{
    public interface IBenchmarkService
    {
        Task<Response> CreateBenchmark(CreateBenchmarkDto benchmark, string token);
        Task<Response> DeleteBenchmark(Guid id);
        Task<DataResponse<BenchmarkDto>> GetBenchmark(Guid id);
        Task<DataResponse<IEnumerable<BenchmarkDto>>> GetBenchmarksPaged(int page, int size);
    }
}
