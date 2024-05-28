using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Responses;
using PheasantBench.Application.ViewModels;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;

namespace PheasantBench.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        public UserService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }
        public async Task<Response> DeleteUser(Guid id)
        {
            Response response = new Response();

            User? user = await _UserRepository.GetByIdAsync(id, true);

            if (user is null)
            {
                response.Success = false;
                response.ErrorMessage = ResponseConstants.UserNotFound;
                return response;
            }

            if (!await _UserRepository.DeleteAsync(user))
            {
                response.Success = false;
                response.ErrorMessage = ResponseConstants.Unexpected;
                return response;
            }

            return response;
        }

        public async Task<DataResponse<UserDto>> GetUser(Guid id)
        {
            DataResponse<UserDto> response = new DataResponse<UserDto>();

            User? user = await _UserRepository.GetByIdAsync(id, true);

            if (user is null)
            {
                response.Success = false;
                response.ErrorMessage = ResponseConstants.UserNotFound;
                return response;
            }

            response.Data = new UserDto()
            {
                Name = user.NormalizedUserName
            };

            return response;
        }

        public async Task<DataResponse<IEnumerable<UserDto>>> GetUsersPaged(int page, int size)
        {
            DataResponse<IEnumerable<UserDto>> response = new DataResponse<IEnumerable<UserDto>>();

            var users = await _UserRepository.GetPagedAsync(false, page, size);

            if (!users.Any())
            {
                response.Success = false;
                response.ErrorMessage = ResponseConstants.UserNotFound;
                return response;
            }

            response.Data = users.Select(x => new UserDto()
            {
                Name = x.UserName,
            });

            return response;
        }
    }
}
