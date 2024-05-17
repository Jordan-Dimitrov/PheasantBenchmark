using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;

namespace PheasantBench.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenchmarkApiController : ControllerBase
    {
        private readonly ITokenService _TokenService;
        private readonly IBenchmarkService _BenchmarkService;

        public BenchmarkApiController(ITokenService tokenService,
            IBenchmarkService benchmarkService)
        {
            _TokenService = tokenService;
            _BenchmarkService = benchmarkService;
        }

        [HttpPost]
        public async Task<IActionResult> PostBenchmark(CreateBenchmarkDto benchmarkDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? jwtToken = Request.Cookies["jwtToken"];

            string username = _TokenService.GetUsername(jwtToken);

            if (username is null)
            {
                return BadRequest(ModelState);
            }

            var response = await _BenchmarkService.CreateBenchmark(benchmarkDto, username);

            if (!response.Success)
            {
                return BadRequest();
            }

            return Ok(response);
        }
    }
}
