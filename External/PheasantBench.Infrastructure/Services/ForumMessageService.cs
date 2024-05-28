using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;
using PheasantBench.Application.ViewModels;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;

namespace PheasantBench.Infrastructure.Services
{
    public class ForumMessageService : IForumMessageService
    {
        private readonly IForumMessageRepository _ForumMessageRepository;
        private readonly IFileService _FileService;
        private readonly IUserRepository _UserRepository;
        private readonly IForumThreadRepository _ForumThreadRepository;
        public ForumMessageService(IForumMessageRepository forumMessageRepository,
            IFileService fileService, IUserRepository userRepository,
            IForumThreadRepository forumThreadRepository)
        {
            _ForumMessageRepository = forumMessageRepository;
            _FileService = fileService;
            _UserRepository = userRepository;
            _ForumThreadRepository = forumThreadRepository;
        }
        public async Task<Response> CreateForumMessage(CreateForumMessageDto benchmark, string userId)
        {
            Response response = new Response();

            var user = await _UserRepository.GetByIdAsync(Guid.Parse(userId), true);

            var thread = await _ForumThreadRepository.GetByIdAsync(benchmark.ForumThreadId, true);

            if (user is null)
            {
                response.Success = false;
                response.ErrorMessage = ResponseConstants.UserNotFound;
                return response;
            }

            if (thread is null)
            {
                response.Success = false;
                response.ErrorMessage = ResponseConstants.ThreadNotFound;
                return response;
            }

            ForumMessage forumMessage = new ForumMessage()
            {
                MessageContent = benchmark.MessageContent,
                DateCreated = DateTime.UtcNow,
                ForumThreadId = benchmark.ForumThreadId,
                UpvoteCount = 0,
                User = user
            };

            if (benchmark.File is not null)
            {
                var fileResponse = await _FileService.UploadAsync(benchmark.File);

                if (!fileResponse.Success)
                {
                    return fileResponse;
                }

                forumMessage.FileName = fileResponse.Data;
            }

            if (!await _ForumMessageRepository.InsertAsync(forumMessage))
            {
                response.Success = false;
                response.ErrorMessage = ResponseConstants.Unexpected;
            }

            return new Response();
        }

        public async Task<DataResponse<Guid>> DeleteForumMessage(Guid id)
        {
            DataResponse<Guid> response = new DataResponse<Guid>();

            ForumMessage? benchmark = await _ForumMessageRepository.GetByIdAsync(id, true);

            if (benchmark is null)
            {
                response.Success = false;
                response.ErrorMessage = ResponseConstants.MessageNotFound;
                return response;
            }

            if (benchmark.FileName is not null)
            {
                var fileResponse = await _FileService.RemoveAsync(benchmark.FileName);

                if (!fileResponse.Success)
                {
                    return response;
                }
            }

            if (!await _ForumMessageRepository.DeleteAsync(benchmark))
            {
                response.Success = false;
                response.ErrorMessage = ResponseConstants.Unexpected;
            }

            response.Data = benchmark.ForumThreadId;

            return response;
        }

        public async Task<DataResponse<ForumMessageDto>> GetBForumMessage(Guid id)
        {
            DataResponse<ForumMessageDto> response = new DataResponse<ForumMessageDto>();
            response.Data = new ForumMessageDto();

            ForumMessage? benchmark = await _ForumMessageRepository.GetByIdAsync(id, false);

            if (benchmark is null)
            {
                response.Success = false;
                response.ErrorMessage = ResponseConstants.MessageNotFound;
                return response;
            }

            response.Data = new ForumMessageDto()
            {
                Id = benchmark.Id,
                MessageContent = benchmark.MessageContent,
                DateCreated = DateTime.UtcNow,
                FileName = benchmark.FileName,
                ForumThreadId = benchmark.ForumThreadId,
                UpvoteCount = benchmark.UpvoteCount,
                User = new UserDto { Name = benchmark.User.UserName }
            };

            return response;
        }

        public async Task<DataResponse<ForumMessagesPagedDto>> GetForumMessagesPaged(int page, int size)
        {
            DataResponse<ForumMessagesPagedDto> response = new DataResponse<ForumMessagesPagedDto>();
            response.Data = new ForumMessagesPagedDto();

            var benchmark = await _ForumMessageRepository.GetPagedAsync(false, page, size);

            if (benchmark is null)
            {
                response.Success = false;
                response.ErrorMessage = ResponseConstants.MessageNotFound;
                return response;
            }

            response.Data.ForumMessages = benchmark.Select(x => new ForumMessageDto()
            {
                Id = x.Id,
                MessageContent = x.MessageContent,
                DateCreated = x.DateCreated,
                FileName = x.FileName,
                ForumThreadId = x.ForumThreadId,
                UpvoteCount = x.UpvoteCount,
                User = new UserDto { Name = x.User.UserName }

            });

            response.Data.TotalPages = await _ForumMessageRepository.GetPageCount(size);

            return response;
        }

        public async Task<DataResponse<ForumMessagesPagedDto>> GetForumMessagesPagedByThread(int page, int size, Guid threadId)
        {
            DataResponse<ForumMessagesPagedDto> response = new DataResponse<ForumMessagesPagedDto>();
            response.Data = new ForumMessagesPagedDto();

            var benchmark = await _ForumMessageRepository.GetPagedByThreadAsync(page, size, threadId, false);

            if (benchmark is null)
            {
                response.Success = false;
                response.ErrorMessage = ResponseConstants.MessageNotFound;
                return response;
            }

            response.Data.ForumMessages = benchmark.Select(x => new ForumMessageDto()
            {
                Id = x.Id,
                MessageContent = x.MessageContent,
                DateCreated = x.DateCreated,
                FileName = x.FileName,
                ForumThreadId = x.ForumThreadId,
                UpvoteCount = x.UpvoteCount,
                User = new UserDto { Name = x.User.UserName }
            });

            response.Data.TotalPages = await _ForumMessageRepository
                .GetMessageCountByThread(threadId, size);

            return response;
        }
    }
}
