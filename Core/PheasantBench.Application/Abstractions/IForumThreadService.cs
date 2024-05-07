using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;

namespace PheasantBench.Application.Abstractions
{
    public interface IForumThreadService
    {
        Task<Response> CreateForumThread(CreateForumThreadDto benchmark);
        Task<Response> DeleteForumThread(Guid id);
        Task<DataResponse<ForumThreadDto>> GetForumThread(Guid id);
        Task<DataResponse<IEnumerable<ForumThreadDto>>> GetForumThreadsPaged(int page, int size);
    }
}
