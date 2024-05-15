using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;

namespace PheasantBench.Web.Controllers
{
    public class BenchmarkController : Controller
    {
        private const int _Size = 5;
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

        [HttpGet]
        public IActionResult Download()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile()
        {
            var response = await _FileService.DownloadBenchmark();

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
                return RedirectToAction("Error");
            }

            return response.Data;
        }

        [HttpPost]
        public async Task<IActionResult> PostBenchmark(CreateBenchmarkDto benchmarkDto)
        {
            string? jwtToken = Request.Cookies["jwtToken"];

            string username = _TokenService.GetUsername(jwtToken);

            if (username is null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _BenchmarkService.CreateBenchmark(benchmarkDto, username);

            if (!response.Success)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBenchmarks([FromQuery] int page = 1)
        {
            var response = await _BenchmarkService.GetBenchmarksPaged(page, _Size);

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
                return RedirectToAction("Error");
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
