using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Domain.Models;

namespace PheasantBench.Infrastructure.Services
{
    public class BenchmarkService : IBenchmarkService
    {
        private readonly IBenchmarkRepository _BenchmarkRepository;
        private readonly IUserRepository _UserRepository;
        private readonly ITokenService _TokenService;
        public BenchmarkService(IUserRepository userRepository,
            IBenchmarkRepository benchmarkRepository,
            ITokenService tokenService)
        {
            _UserRepository = userRepository;
            _BenchmarkRepository = benchmarkRepository;
            _TokenService = tokenService;
        }
        public async Task<Response> CreateBenchmark(CreateBenchmarkDto benchmarkDto, string token)
        {
            User? user = await _UserRepository.GetByAsync(x => x.UserName == _TokenService.GetUsername(token));

            if (user is null)
            {

            }

            Benchmark benchmark = new Benchmark()
            {
                Architecture = benchmarkDto.Architecture,
                DateCreated = DateTime.UtcNow,
                MachineName = benchmarkDto.MachineName,
                OsVersion = benchmarkDto.OsVersion,
                ProcessorName = benchmarkDto.ProcessorName,
                Score = benchmarkDto.Score,
                User = await _UserRepository.GetByAsync(x => x.UserName == _TokenService.GetUsername(token))
            };

            return new Response();
        }

        Task<Response> IBenchmarkService.DeleteBenchmark(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<DataResponse<BenchmarkDto>> IBenchmarkService.GetBenchmark(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<DataResponse<ICollection<BenchmarkDto>>> IBenchmarkService.GetBenchmarksPaged(int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}
