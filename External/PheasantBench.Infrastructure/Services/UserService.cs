using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;

namespace PheasantBench.Infrastructure.Services
{
    public class UserService : IUserService
    {
        Task<Response> IUserService.DeleteBenchmark(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<DataResponse<UserDto>> IUserService.GetBenchmark(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<DataResponse<ICollection<UserDto>>> IUserService.GetBenchmarksPaged(int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}
