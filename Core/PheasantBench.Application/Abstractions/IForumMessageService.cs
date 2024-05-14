using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;
using PheasantBench.Application.ViewModels;

namespace PheasantBench.Application.Abstractions
{
    public interface IForumMessageService
    {
        Task<Response> CreateForumMessage(CreateForumMessageDto benchmark, string userId);
        Task<DataResponse<Guid>> DeleteForumMessage(Guid id);
        Task<DataResponse<ForumMessageDto>> GetBForumMessage(Guid id);
        Task<DataResponse<ForumMessagesPagedDto>> GetForumMessagesPaged(int page, int size);
        Task<DataResponse<ForumMessagesPagedDto>> GetForumMessagesPagedByThread(int page, int size, Guid threadId);
    }
}
