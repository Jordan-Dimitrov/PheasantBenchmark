using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;

namespace PheasantBench.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public async Task<Response> DeleteBenchmark(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<UserDto>> GetBenchmark(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<IEnumerable<UserDto>>> GetBenchmarksPaged(int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}
