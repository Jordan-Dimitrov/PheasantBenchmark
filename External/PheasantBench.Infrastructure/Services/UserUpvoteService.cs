using Azure.Core;
using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<Response> UpvoteAsync(ForumMessage forumMessage, User user, CreateUserUpvoteDto value)
        {
            Response response = new Response();
            response.Success = false;

            if (value.Score != 1 && value.Score != -1 && value.Score != 0)
            {
                response.ErrorMessage = "Invalid value";
                return response;
            }

            if (user is null)
            {
                response.ErrorMessage = "Invalid user";
                return response;
            }

            if (forumMessage is null)
            {
                response.ErrorMessage = "Invalid message";
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
                forumMessage.UpvoteCount += value.Score;

                if (!await _UserUpvoteRepository.InsertAsync(upvoteToAdd))
                {
                    response.ErrorMessage = "Unexpected error";
                    return response;
                }

                response.Success = true;
                return response;
            }

            if (upvote.Rating == value.Score)
            {
                response.Success = true;
                return response;
            }

            forumMessage.UpvoteCount -= upvote.Rating;
            forumMessage.UpvoteCount += value.Score;
            upvote.Rating = value.Score;

            if (!await _UserUpvoteRepository.UpdateAsync(upvote)
                || !await _ForumMessageRepository.UpdateAsync(forumMessage))
            {
                response.ErrorMessage = "Unexpected error";
                return response;
            }

            response.Success = true;
            return response;
        }
    }
}
