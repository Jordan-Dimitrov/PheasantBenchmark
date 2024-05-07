using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;

namespace PheasantBench.Web.Controllers
{
    public class ThreadController : Controller
    {
        private readonly IForumThreadService _ForumThreadService;
        private readonly IForumMessageService _ForumMessageService;
        private readonly IUserRepository _UserRepository;
        private readonly IForumThreadRepository _ForumThreadRepository;
        public ThreadController(IForumThreadService forumThreadService,
            IForumMessageService forumMessageService,
            IUserRepository userRepository,
            IForumThreadRepository forumThreadRepository)
        {
            _ForumThreadService = forumThreadService;
            _ForumMessageService = forumMessageService;
            _UserRepository = userRepository;
            _ForumThreadRepository = forumThreadRepository;
        }
        public async Task<IActionResult> Create()
        {
            await _ForumThreadService.CreateForumThread(new CreateForumThreadDto() {Name = "tomaaa", Description = "Kude e tomaaa" });

            var temp = await _ForumThreadRepository.GetAllAsync(true);

            var temp2 = await _UserRepository.GetAllAsync(true);

            _ForumMessageService.CreateForumMessage(new CreateForumMessageDto()
            { MessageContent = "tomaaa", ForumThreadId = temp.First().Id }, temp2.First());
            return View();
        }
    }
}
