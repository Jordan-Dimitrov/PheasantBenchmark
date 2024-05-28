using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;

namespace PheasantBench.Infrastructure.Services
{
    public class UserUpvoteService : IUserUpvoteService
    {
        private readonly IUserUpvoteRepository _UserUpvoteRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IForumMessageRepository _ForumMessageRepository;
        public UserUpvoteService(IUserRepository userRepository, IUserUpvoteRepository userUpvoteRepository,
            IForumMessageRepository forumMessageRepository)
        {
            _ForumMessageRepository = forumMessageRepository;
            _UserRepository = userRepository;
            _UserUpvoteRepository = userUpvoteRepository;
        }
        public async Task<DataResponse<Guid>> UpvoteAsync(Guid forumMessageId, string userId, CreateUserUpvoteDto value)
        {
            DataResponse<Guid> response = new DataResponse<Guid>();

            response.Success = false;

            var user = await _UserRepository.GetByIdAsync(Guid.Parse(userId), true);
            var forumMessage = await _ForumMessageRepository.GetByIdAsync(forumMessageId, true);

            if (value.Score != 1 && value.Score != 0)
            {
                response.ErrorMessage = "Invalid value";
                return response;
            }

            if (user is null)
            {
                response.ErrorMessage = ResponseConstants.UserNotFound;
                return response;
            }

            if (forumMessage is null)
            {
                response.ErrorMessage = ResponseConstants.MessageNotFound;
                return response;
            }

            UserUpvotes? upvote = await _UserUpvoteRepository
                .GetUserUpvoteByMessageAndUserAsync(user.Id, forumMessage.Id);

            if (upvote is null)
            {
                UserUpvotes upvoteToAdd = new UserUpvotes();
                upvoteToAdd.Rating = value.Score;
                upvoteToAdd.ForumMessage = forumMessage;
                upvoteToAdd.User = user;
                forumMessage.UpvoteCount += value.Score == 0 ? -1 : 1;

                if (!await _UserUpvoteRepository.InsertAsync(upvoteToAdd))
                {
                    response.ErrorMessage = ResponseConstants.Unexpected;
                    return response;
                }

                response.Success = true;
                response.Data = upvoteToAdd.ForumMessage.ForumThreadId;
                return response;
            }

            if (upvote.Rating == value.Score)
            {
                response.Success = true;
                response.Data = upvote.ForumMessage.ForumThreadId;
                return response;
            }

            forumMessage.UpvoteCount -= upvote.Rating == 0 ? -1 : 1;
            forumMessage.UpvoteCount += value.Score == 0 ? -1 : 1;
            upvote.Rating = value.Score;

            if (!await _UserUpvoteRepository.UpdateAsync(upvote)
                || !await _ForumMessageRepository.UpdateAsync(forumMessage))
            {
                response.ErrorMessage = ResponseConstants.Unexpected;
                return response;
            }

            response.Data = upvote.ForumMessage.ForumThreadId;
            response.Success = true;
            return response;
        }
    }
}
