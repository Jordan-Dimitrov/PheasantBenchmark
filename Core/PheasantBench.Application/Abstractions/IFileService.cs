using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Responses;

namespace PheasantBench.Application.Abstractions
{
    public interface IFileService
    {
        Task<Response> UploadAsync(IFormFile file);
        Task<Response> RemoveAsync(string filename);
        Task<DataResponse<FileContentResult>> GetAsync(string filename);
    }
}
