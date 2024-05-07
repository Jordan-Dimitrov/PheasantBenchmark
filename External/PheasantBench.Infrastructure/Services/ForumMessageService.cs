using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;

namespace PheasantBench.Infrastructure.Services
{
    public class ForumMessageService : IForumMessageService
    {
        public ForumMessageService()
        {

        }
        public async Task<Response> CreateForumMessage(CreateBenchmarkDto benchmark, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> DeleteForumMessage(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<BenchmarkDto>> GetBForumMessage(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<IEnumerable<BenchmarkDto>>> GetForumMessagesPaged(int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}
