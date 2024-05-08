using Microsoft.AspNetCore.Http;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;
using PheasantBench.Application.ViewModels;

namespace PheasantBench.Application.Abstractions
{
    public interface IForumMessageService
    {
        Task<Response> CreateForumMessage(CreateForumMessageDto benchmark, string userId);
        Task<Response> CreateForumMessageWithFile(CreateForumMessageDto benchmark, string userId, IFormFile file);
        Task<Response> DeleteForumMessage(Guid id);
        Task<DataResponse<ForumMessageDto>> GetBForumMessage(Guid id);
        Task<DataResponse<IEnumerable<ForumMessageDto>>> GetForumMessagesPaged(int page, int size);
        Task<DataResponse<IEnumerable<ForumMessageDto>>> GetForumMessagesPagedByThread(int page, int size, Guid threadId);
    }
}
