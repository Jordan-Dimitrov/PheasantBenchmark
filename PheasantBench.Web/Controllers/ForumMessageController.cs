using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Domain.Models;
using PheasantBench.Infrastructure.Services;

namespace PheasantBench.Web.Controllers
{
    public class ForumMessageController : Controller
    {
        private const int _Size = 5;
        private readonly IForumMessageService _ForumMessageService;
        private readonly IForumThreadService _ForumThreadService;
        private readonly IUserUpvoteService _UserUpvoteService;

        public ForumMessageController(IForumMessageService forumMessageService,
            IForumThreadService forumThreadService,
            IUserUpvoteService userUpvoteService)
        {
            _ForumMessageService = forumMessageService;
            _ForumThreadService = forumThreadService;
            _UserUpvoteService = userUpvoteService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create([FromQuery] Guid threadId)
        {
            ViewBag.threadId = threadId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upvote(Guid messageId, byte score)
        {
            var response = await _UserUpvoteService
                .UpvoteAsync(messageId, User.Identity.GetUserId(), new CreateUserUpvoteDto { Score = score });

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return RedirectToAction("GetThreads", "Thread");
            }

            ViewBag.Success = "Addedd successfully";
            return RedirectToAction("GetThreads", "Thread");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(Guid forumMessageId)
        {
            var response = await _ForumMessageService.GetBForumMessage(forumMessageId);

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return View();
            }

            return View(response.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid forumMessageId)
        {
            var response = await _ForumMessageService.DeleteForumMessage(forumMessageId);

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return RedirectToAction("Remove");
            }

            ViewBag.Success = "Removed successfully";

            return RedirectToAction("GetThreads", "Thread");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMessages([FromQuery] int page, [FromQuery] Guid threadId)
        {
            var response = await _ForumMessageService.GetForumMessagesPagedByThread(page, _Size, threadId);
            var thread = await _ForumThreadService.GetForumThread(threadId);

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return View();
            }

            ViewBag.PageNumber = page;
            ViewBag.Description = thread.Data.Description;
            ViewBag.ThreadId = threadId;

            return View(response.Data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateForumMessageDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Invalid data";
                return View();
            }

            var response = await _ForumMessageService.CreateForumMessage(dto, User.Identity.GetUserId());

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return View();
            }

            ViewBag.Success = "Successfuly created";

            return RedirectToAction("GetMessages", new { page = 1, threadId = dto.ForumThreadId });
        }

    }
}
