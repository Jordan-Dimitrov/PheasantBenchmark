using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;
using PheasantBench.Infrastructure.Repositories;

namespace PheasantBench.Infrastructure.Services
{
    public class ForumThreadService : IForumThreadService
    {
        private readonly IForumThreadRepository _ForumThreadRepository;
        public ForumThreadService(IForumThreadRepository forumThreadRepository)
        {
            _ForumThreadRepository = forumThreadRepository;
        }

        public async Task<Response> CreateForumThread(CreateForumThreadDto benchmark)
        {
            Response response = new Response();

            if(await _ForumThreadRepository.ExistsAsync(x => x.Name == benchmark.Name))
            {
                response.Success = false;
                response.ErrorMessage = "Forum thread already exists";
                return response;
            }

            ForumThread forumThread = new ForumThread() 
            {
                Name = benchmark.Name,
                Description = benchmark.Description,
            };

            if (!await _ForumThreadRepository.InsertAsync(forumThread))
            {
                response.Success = false;
                response.ErrorMessage = "Unexpected error";
            }

            return response;
        }

        public async Task<Response> DeleteForumThread(Guid id)
        {
            Response response = new Response();

            ForumThread? benchmark = await _ForumThreadRepository.GetByIdAsync(id, true);

            if (benchmark is null)
            {
                response.Success = false;
                response.ErrorMessage = "Forum thread not found";
                return response;
            }

            if (!await _ForumThreadRepository.DeleteAsync(benchmark))
            {
                response.Success = false;
                response.ErrorMessage = "Unexpected error";
            }

            return response;
        }

        public async Task<DataResponse<ForumThreadDto>> GetForumThread(Guid id)
        {
            DataResponse<ForumThreadDto> response = new DataResponse<ForumThreadDto>();

            ForumThread? benchmark = await _ForumThreadRepository.GetByIdAsync(id, false);

            if (benchmark is null)
            {
                response.Success = false;
                response.ErrorMessage = "No such forum thread";
                return response;
            }

            response.Data = new ForumThreadDto()
            {
                Id = benchmark.Id,
                Name = benchmark.Name,
                Description = benchmark.Description,
            };

            return response;
        }

        public async Task<DataResponse<IEnumerable<ForumThreadDto>>> GetForumThreadsPaged(int page, int size)
        {
            DataResponse<IEnumerable<ForumThreadDto>> response = new DataResponse<IEnumerable<ForumThreadDto>>();

            var benchmark = await _ForumThreadRepository.GetPagedAsync(false, page, size);

            if (benchmark is null)
            {
                response.Success = false;
                response.ErrorMessage = "No such threads";
                return response;
            }

            response.Data = benchmark
                .Select(x => new ForumThreadDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            });

            return response;
        }
    }
}
