using Microsoft.AspNet.Identity;
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

        public ForumMessageController(IForumMessageService forumMessageService,
            IForumThreadService forumThreadService)
        {
            _ForumMessageService = forumMessageService;
            _ForumThreadService = forumThreadService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery] int page, [FromQuery] Guid threadId)
        {
            var response = await _ForumMessageService.GetForumMessagesPagedByThread(page, _Size, threadId);
            var thread = await _ForumThreadService.GetForumThread(threadId);

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return RedirectToAction("Create");
            }

            ViewBag.PageNumber = page;
            ViewBag.Description = thread.Data.Description;

            return View(response.Data);
        }

        [HttpPost]
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

            return View();
        }

    }
}
