using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;

namespace PheasantBench.Application.Abstractions
{
    public interface IUserService
    {
        Task<Response> DeleteUser(Guid id);
        Task<DataResponse<UserDto>> GetUser(Guid id);
        Task<DataResponse<IEnumerable<UserDto>>> GetUsersPaged(int page, int size);
    }
}
