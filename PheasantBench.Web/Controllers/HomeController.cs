using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.ViewModels;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Web.Models;
using System.Diagnostics;

namespace PheasantBench.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBenchmarkRepository _BenchmarkRepository;

        public HomeController(ILogger<HomeController> logger, IBenchmarkRepository benchmarkRepository)
        {
            _logger = logger;
            _BenchmarkRepository = benchmarkRepository;
        }

        public async Task<IActionResult> Index()
        {
            int count = await _BenchmarkRepository.GetCount();
            var result = new StatsDto() { BenchmarkCount = count };

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
