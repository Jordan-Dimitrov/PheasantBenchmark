using PheasantBench.Application.Responses;
using PheasantBench.Application.ViewModels;

namespace PheasantBench.Application.Abstractions
{
    public interface IUserService
    {
        Task<Response> DeleteUser(Guid id);
        Task<DataResponse<UserDto>> GetUser(Guid id);
        Task<DataResponse<IEnumerable<UserDto>>> GetUsersPaged(int page, int size);
    }
}
