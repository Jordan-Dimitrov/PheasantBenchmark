using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;
using PheasantBench.Application.ViewModels;
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

            Response response = new Response();

            if (user is null)
            {
                response.Success = false;
                response.ErrorMessage = "User not found";
                return response;
            }

            Benchmark benchmark = new Benchmark()
            {
                Architecture = benchmarkDto.Architecture,
                DateCreated = DateTime.UtcNow,
                MachineName = benchmarkDto.MachineName,
                OsVersion = benchmarkDto.OsVersion,
                ProcessorName = benchmarkDto.ProcessorName,
                Score = benchmarkDto.Score,
                User = user
            };

            if (!await _BenchmarkRepository.InsertAsync(benchmark))
            {
                response.Success = false;
                response.ErrorMessage = "Unexpected error";
            }

            return new Response();
        }

        public async Task<Response> DeleteBenchmark(Guid id)
        {
            Response response = new Response();

            Benchmark? benchmark = await _BenchmarkRepository.GetByIdAsync(id, true);

            if (benchmark is null)
            {
                response.Success = false;
                response.ErrorMessage = "Benchmark not found";
                return response;
            }

            if (!await _BenchmarkRepository.DeleteAsync(benchmark))
            {
                response.Success = false;
                response.ErrorMessage = "Unexpected error";
            }

            return response;
        }

        public async Task<DataResponse<BenchmarkDto>> GetBenchmark(Guid id)
        {
            DataResponse<BenchmarkDto> response = new DataResponse<BenchmarkDto>();

            Benchmark? benchmark = await _BenchmarkRepository.GetByIdAsync(id, false);

            if (benchmark is null)
            {
                response.Success = false;
                response.ErrorMessage = "No such benchmark";
                return response;
            }

            response.Data = new BenchmarkDto()
            {
                Architecture = benchmark.Architecture,
                DateCreated = benchmark.DateCreated,
                MachineName = benchmark.MachineName,
                OsVersion = benchmark.OsVersion,
                ProcessorName = benchmark.ProcessorName,
                Score = benchmark.Score,
                User = new UserDto
                {
                    Name = benchmark.User.UserName,
                }
            };

            return response;
        }

        public async Task<DataResponse<IEnumerable<BenchmarkDto>>> GetBenchmarksPaged(int page, int size)
        {
            DataResponse<IEnumerable<BenchmarkDto>> response = new DataResponse<IEnumerable<BenchmarkDto>>();

            var benchmark = await _BenchmarkRepository.GetPagedAsync(false, page, size);

            if (!benchmark.Any())
            {
                response.Success = false;
                response.ErrorMessage = "No such benchmarks";
                return response;
            }

            response.Data = benchmark.Select(x => new BenchmarkDto()
            {
                Architecture = x.Architecture,
                DateCreated = x.DateCreated,
                MachineName = x.MachineName,
                OsVersion = x.OsVersion,
                ProcessorName = x.ProcessorName,
                Score = x.Score,
                User = new UserDto
                {
                    Name = x.User.UserName,
                }
            });

            return response;
        }
    }
}
