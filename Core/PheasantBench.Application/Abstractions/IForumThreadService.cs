using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;
using PheasantBench.Application.ViewModels;

namespace PheasantBench.Application.Abstractions
{
    public interface IForumThreadService
    {
        Task<Response> CreateForumThread(CreateForumThreadDto benchmark);
        Task<Response> DeleteForumThread(Guid id);
        Task<DataResponse<ForumThreadDto>> GetForumThread(Guid id);
        Task<DataResponse<ForumThreadPagedDto>> GetForumThreadsPaged(int page, int size);
    }
}
