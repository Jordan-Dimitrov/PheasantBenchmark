﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Infrastructure;

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

        [HttpGet]
        [Authorize]
        public IActionResult Create([FromQuery] Guid threadId)
        {
            ViewBag.threadId = threadId;

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upvote(Guid messageId, byte score)
        {
            var response = await _UserUpvoteService
                .UpvoteAsync(messageId, User.Identity.GetUserId(), new CreateUserUpvoteDto { Score = score });

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
            }
            else
            {
                TempData["Success"] = ResponseConstants.UpvoteSuccess;
            }

            return RedirectToAction("GetMessages", new { forumPage = 1, threadId = response.Data });
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
                TempData["ErrorMessage"] = response.ErrorMessage;
            }
            else
            {
                TempData["Success"] = ResponseConstants.RemoveSuccess;
            }

            return RedirectToAction("GetMessages", new { forumPage = 1, threadId = response.Data });
        }

        public IActionResult Error()
        {
            string errorMessage = (string)TempData["ErrorMessage"];

            ViewBag.ErrorMessage = errorMessage;

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMessages([FromQuery] Guid threadId, [FromQuery] int forumPage = 1)
        {
            var response = await _ForumMessageService.GetForumMessagesPagedByThread(forumPage, _Size, threadId);
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

            string success = (string)TempData["Success"];

            ViewBag.Success = success;

            string error = (string)TempData["ErrorMessage"];

            ViewBag.ErrorMessage = error;

            response.Data.PageNumber = forumPage;
            response.Data.Thread = thread.Data;

            return View(response.Data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateForumMessageDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ResponseConstants.InvalidData;
                return RedirectToAction("GetMessages", new { forumPage = 1, threadId = dto.ForumThreadId });
            }

            var response = await _ForumMessageService.CreateForumMessage(dto, User.Identity.GetUserId());

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
            }
            else
            {
                TempData["Success"] = ResponseConstants.CreateSuccess;
            }

            return RedirectToAction("GetMessages", new { forumPage = 1, threadId = dto.ForumThreadId });
        }

    }
}
