using Microsoft.AspNetCore.Http;
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
        public ForumMessageService(IForumMessageRepository forumMessageRepository,
            IFileService fileService, IUserRepository userRepository)
        {
            _ForumMessageRepository = forumMessageRepository;
            _FileService = fileService;
            _UserRepository = userRepository;
        }
        public async Task<Response> CreateForumMessageWithFile(CreateForumMessageDto benchmark, string userId, IFormFile file)
        {
            Response response = new Response();

            var user = await _UserRepository.GetByIdAsync(Guid.Parse(userId), true);

            if (user is null)
            {
                response.Success = false;
                response.ErrorMessage = "User not found";
                return response;
            }

            ForumMessage forumMessage = new ForumMessage()
            {
                MessageContent = benchmark.MessageContent,
                DateCreated = DateTime.UtcNow,
                FileName = file.FileName,
                ForumThreadId = benchmark.ForumThreadId,
                UpvoteCount = 0,
                User = user
            };

            var fileResponse = await _FileService.UploadAsync(file);

            if (!fileResponse.Success)
            {
                return fileResponse;
            }

            if (!await _ForumMessageRepository.InsertAsync(forumMessage))
            {
                response.Success = false;
                response.ErrorMessage = "Unexpected error";
            }

            return new Response();
        }

        public async Task<Response> CreateForumMessage(CreateForumMessageDto benchmark, string userId)
        {
            Response response = new Response();

            var user = await _UserRepository.GetByIdAsync(Guid.Parse(userId), true);

            if (user is null)
            {
                response.Success = false;
                response.ErrorMessage = "User not found";
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

            if (!await _ForumMessageRepository.InsertAsync(forumMessage))
            {
                response.Success = false;
                response.ErrorMessage = "Unexpected error";
            }

            return new Response();
        }

        public async Task<Response> DeleteForumMessage(Guid id)
        {
            Response response = new Response();

            ForumMessage? benchmark = await _ForumMessageRepository.GetByIdAsync(id, true);

            if (benchmark is null)
            {
                response.Success = false;
                response.ErrorMessage = "Message not found";
                return response;
            }

            if (benchmark.FileName is not null)
            {
                var fileResponse = await _FileService.RemoveAsync(benchmark.FileName);

                if (!fileResponse.Success)
                {
                    return fileResponse;
                }
            }

            if (!await _ForumMessageRepository.DeleteAsync(benchmark))
            {
                response.Success = false;
                response.ErrorMessage = "Unexpected error";
            }

            return response;
        }

        public async Task<DataResponse<ForumMessageDto>> GetBForumMessage(Guid id)
        {
            DataResponse<ForumMessageDto> response = new DataResponse<ForumMessageDto>();

            ForumMessage? benchmark = await _ForumMessageRepository.GetByIdAsync(id, false);

            if (benchmark is null)
            {
                response.Success = false;
                response.ErrorMessage = "No such forum message";
                return response;
            }

            response.Data = new ForumMessageDto()
            {
                MessageContent = benchmark.MessageContent,
                DateCreated = DateTime.UtcNow,
                FileName = benchmark.FileName,
                ForumThreadId = benchmark.ForumThreadId,
                UpvoteCount = 0,
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
                response.ErrorMessage = "No such forum mesasges";
                return response;
            }

            response.Data.ForumMessages = benchmark.Select(x => new ForumMessageDto()
            {
                MessageContent = x.MessageContent,
                DateCreated = x.DateCreated,
                FileName = x.FileName,
                ForumThreadId = x.ForumThreadId,
                UpvoteCount = 0
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
                response.ErrorMessage = "No such forum mesasges";
                return response;
            }

            response.Data.ForumMessages = benchmark.Select(x => new ForumMessageDto()
            {
                MessageContent = x.MessageContent,
                DateCreated = x.DateCreated,
                FileName = x.FileName,
                ForumThreadId = x.ForumThreadId,
                UpvoteCount = 0
            });

            response.Data.TotalPages = await _ForumMessageRepository.GetPageCount(size);

            return response;
        }
    }
}
