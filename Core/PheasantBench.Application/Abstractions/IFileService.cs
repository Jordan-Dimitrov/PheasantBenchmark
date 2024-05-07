using Microsoft.AspNetCore.Http;
using PheasantBench.Application.Responses;

namespace PheasantBench.Application.Abstractions
{
    public interface IFileService
    {
        Task<Response> UploadAsync(IFormFile file);
        Task<Response> RemoveAsync(string filename);
        Task<Response> GetAsync(string filename);
    }
}
