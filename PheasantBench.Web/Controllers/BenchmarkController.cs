using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Abstractions;

namespace PheasantBench.Web.Controllers
{
    public class BenchmarkController : Controller
    {
        private const int _Size = 20;
        private readonly IBenchmarkService _BenchmarkService;
        private readonly ITokenService _TokenService;
        private readonly IFileService _FileService;
        public BenchmarkController(IBenchmarkService benchmarkService,
            ITokenService tokenService, IFileService fileService)
        {
            _BenchmarkService = benchmarkService;
            _TokenService = tokenService;
            _FileService = fileService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Download()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DownloadFile()
        {
            var token = _TokenService.CreateToken(User.Identity.Name);
            var response = await _FileService.DownloadBenchmark(token);

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
                return RedirectToAction("Error", "ForumMessage");
            }

            return response.Data;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBenchmarks([FromQuery] int page = 1)
        {
            var response = await _BenchmarkService.GetBenchmarksPaged(page, _Size);

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
                return RedirectToAction("Error", "ForumMessage");
            }

            ViewBag.PageNumber = page;

            return View(response.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(Guid benchmarkId)
        {
            var response = await _BenchmarkService.GetBenchmark(benchmarkId);

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
        public async Task<IActionResult> Delete(Guid benchmarkId)
        {
            var response = await _BenchmarkService.DeleteBenchmark(benchmarkId);

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return RedirectToAction("Remove");
            }

            ViewBag.Success = "Removed successfully";

            return RedirectToAction("GetBenchmarks");
        }
    }
}
