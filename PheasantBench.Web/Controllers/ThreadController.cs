using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Infrastructure;

namespace PheasantBench.Web.Controllers
{
    public class ThreadController : Controller
    {
        private readonly IForumThreadService _ForumThreadService;
        private readonly IForumMessageService _ForumMessageService;
        private readonly IUserService _UserService;
        private const int _Size = 10;
        public ThreadController(IForumThreadService forumThreadService,
            IForumMessageService forumMessageService,
            IUserService userService)
        {
            _ForumThreadService = forumThreadService;
            _ForumMessageService = forumMessageService;
            _UserService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateForumThreadDto forumThread)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = ResponseConstants.InvalidData;
                return View();
            }

            var response = await _ForumThreadService.CreateForumThread(forumThread);

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return View();
            }

            TempData["Success"] = $"Successfuly created {forumThread.Name}";
            return RedirectToAction("GetThreads");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(Guid threadId)
        {
            var response = await _ForumThreadService.GetForumThread(threadId);

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
        public async Task<IActionResult> Delete(Guid threadId)
        {
            var response = await _ForumThreadService.DeleteForumThread(threadId);

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
            }
            else
            {
                TempData["Success"] = ResponseConstants.RemoveSuccess;
            }

            return RedirectToAction("GetThreads");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetThreads([FromQuery] int page = 1)
        {
            var response = await _ForumThreadService.GetForumThreadsPaged(page, _Size);

            string success = (string)TempData["Success"];

            ViewBag.Success = success;

            string error = (string)TempData["ErrorMessage"];

            ViewBag.ErrorMessage = error;

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return RedirectToAction("Create");
            }

            ViewBag.PageNumber = page;

            return View(response.Data);
        }

    }
}
