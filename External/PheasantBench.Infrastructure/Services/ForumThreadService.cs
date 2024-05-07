using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;

namespace PheasantBench.Infrastructure.Services
{
    public class ForumThreadService : IForumThreadService
    {
        public async Task<Response> CreateForumThread(CreateForumThreadDto benchmark, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> DeleteForumThread(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<ForumThreadDto>> GetForumThread(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<IEnumerable<ForumThreadDto>>> GetForumThreadsPaged(int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}
