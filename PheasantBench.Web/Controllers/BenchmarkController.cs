using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Abstractions;

namespace PheasantBench.Web.Controllers
{
    public class BenchmarkController : Controller
    {
        private readonly IBenchmarkService _BenchmarkService;
        public BenchmarkController(IBenchmarkService benchmarkService)
        {
            _BenchmarkService = benchmarkService;
        }
    }
}
