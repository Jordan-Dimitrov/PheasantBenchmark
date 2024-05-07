using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.ViewModels;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;

namespace PheasantBench.Web.Controllers
{
    public class ThreadController : Controller
    {
        private readonly IForumThreadService _ForumThreadService;
        private readonly IForumMessageService _ForumMessageService;
        private readonly IUserService _UserService;
        public ThreadController(IForumThreadService forumThreadService,
            IForumMessageService forumMessageService,
            IUserService userService)
        {
            _ForumThreadService = forumThreadService;
            _ForumMessageService = forumMessageService;
            _UserService = userService;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateForumThreadDto forumThread)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Invalid input";
                return View();  
            }

            var response = await _ForumThreadService.CreateForumThread(forumThread);

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return View();
            }

            ViewBag.Success = "Added successfully";
            return View();
        }

        [HttpDelete("{threadId:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(Guid threadId)
        {
            var response = await _ForumThreadService.DeleteForumThread(threadId);

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return View();
            }

            ViewBag.Success = "Removed successfully";
            return View();
        }

        public async Task<IActionResult> GetThreads()
        {
            return View();
        }
    }
}
