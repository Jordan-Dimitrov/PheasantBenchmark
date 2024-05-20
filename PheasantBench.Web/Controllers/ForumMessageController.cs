using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;

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
                TempData["ErrorMessage"] = response.ErrorMessage;
                return RedirectToAction("Error");
            }

            ViewBag.Success = "Addedd successfully";
            return RedirectToAction("GetMessages", new { page = 1, threadId = response.Data });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(Guid forumMessageId)
        {
            var response = await _ForumMessageService.GetBForumMessage(forumMessageId);

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
                return RedirectToAction("Error");
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
                TempData["ErrorMessage"] = response.ErrorMessage;
                return RedirectToAction("Error");
            }

            ViewBag.Success = "Removed successfully";

            return RedirectToAction("GetMessages", new { page = 1, threadId = response.Data });
        }

        public IActionResult Error()
        {
            string errorMessage = (string)TempData["ErrorMessage"];

            ViewBag.ErrorMessage = errorMessage;

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMessages([FromQuery] int page, [FromQuery] Guid threadId)
        {
            var response = await _ForumMessageService.GetForumMessagesPagedByThread(page, _Size, threadId);
            var thread = await _ForumThreadService.GetForumThread(threadId);

            if (!thread.Success)
            {
                TempData["ErrorMessage"] = thread.ErrorMessage;
                return RedirectToAction("Error");
            }

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
                return RedirectToAction("Error");
            }

            response.Data.PageNumber = page;
            response.Data.Thread = thread.Data;

            return View(response.Data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateForumMessageDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid data!";
                return View("Error");
            }

            var response = await _ForumMessageService.CreateForumMessage(dto, User.Identity.GetUserId());

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
                return View("Error");
            }

            ViewBag.Success = "Successfuly created";

            return RedirectToAction("GetMessages", new { page = 1, threadId = dto.ForumThreadId });
        }

    }
}
