using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;

namespace PheasantBench.Application.Abstractions
{
    public interface IUserService
    {
        Task<Response> DeleteBenchmark(Guid id);
        Task<DataResponse<UserDto>> GetBenchmark(Guid id);
        Task<DataResponse<IEnumerable<UserDto>>> GetBenchmarksPaged(int page, int size);
    }
}
