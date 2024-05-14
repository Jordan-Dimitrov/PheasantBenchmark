using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Responses;

namespace PheasantBench.Application.Abstractions
{
    public interface IFileService
    {
        Task<DataResponse<string>> UploadAsync(IFormFile file);
        Task<Response> RemoveAsync(string filename);
        Task<DataResponse<FileContentResult>> GetAsync(string filename);
    }
}
