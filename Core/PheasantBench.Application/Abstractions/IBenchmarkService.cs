using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;
using PheasantBench.Application.ViewModels;

namespace PheasantBench.Application.Abstractions
{
    public interface IBenchmarkService
    {
        Task<Response> CreateBenchmark(CreateBenchmarkDto benchmark, string token);
        Task<Response> DeleteBenchmark(Guid id);
        Task<DataResponse<BenchmarkDto>> GetBenchmark(Guid id);
        Task<DataResponse<BencmarksPagedDto>> GetBenchmarksPaged(int page, int size);
    }
}
