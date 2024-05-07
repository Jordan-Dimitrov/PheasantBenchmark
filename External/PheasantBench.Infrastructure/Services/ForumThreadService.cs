using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;

namespace PheasantBench.Infrastructure.Services
{
    public class ForumThreadService : IForumThreadService
    {
        Task<Response> IForumThreadService.CreateForumThread(CreateForumThreadDto benchmark, string token)
        {
            throw new NotImplementedException();
        }

        Task<Response> IForumThreadService.DeleteForumThread(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<DataResponse<ForumThreadDto>> IForumThreadService.GetForumThread(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<DataResponse<ICollection<ForumThreadDto>>> IForumThreadService.GetForumThreadsPaged(int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}
