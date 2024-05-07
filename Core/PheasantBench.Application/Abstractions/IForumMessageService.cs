using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;

namespace PheasantBench.Application.Abstractions
{
    public interface IForumMessageService
    {
        Task<Response> CreateForumMessage(CreateBenchmarkDto benchmark, string token);
        Task<Response> DeleteForumMessage(Guid id);
        Task<DataResponse<BenchmarkDto>> GetBForumMessage(Guid id);
        Task<DataResponse<IEnumerable<BenchmarkDto>>> GetForumMessagesPaged(int page, int size);
    }
}
