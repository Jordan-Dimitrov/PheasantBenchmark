using Microsoft.AspNetCore.Http;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;
using PheasantBench.Domain.Models;

namespace PheasantBench.Application.Abstractions
{
    public interface IForumMessageService
    {
        Task<Response> CreateForumMessage(CreateForumMessageDto benchmark, User user);
        Task<Response> CreateForumMessageWithFile(CreateForumMessageDto benchmark, User user, IFormFile file);
        Task<Response> DeleteForumMessage(Guid id);
        Task<DataResponse<ForumMessageDto>> GetBForumMessage(Guid id);
        Task<DataResponse<IEnumerable<ForumMessageDto>>> GetForumMessagesPaged(int page, int size);
        Task<DataResponse<IEnumerable<ForumMessageDto>>> GetForumMessagesPagedByThread(int page, int size, Guid threadId);
    }
}
