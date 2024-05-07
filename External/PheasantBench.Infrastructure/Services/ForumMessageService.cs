using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Dtos;
using PheasantBench.Application.Responses;

namespace PheasantBench.Infrastructure.Services
{
    public class ForumMessageService : IForumMessageService
    {
        Task<Response> IForumMessageService.CreateForumMessage(CreateBenchmarkDto benchmark, string token)
        {
            throw new NotImplementedException();
        }

        Task<Response> IForumMessageService.DeleteForumMessage(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<DataResponse<BenchmarkDto>> IForumMessageService.GetBForumMessage(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<DataResponse<ICollection<BenchmarkDto>>> IForumMessageService.GetForumMessagesPaged(int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}
