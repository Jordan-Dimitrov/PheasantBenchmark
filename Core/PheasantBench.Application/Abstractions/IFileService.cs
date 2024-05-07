using Microsoft.AspNetCore.Http;

namespace PheasantBench.Application.Abstractions
{
    public interface IFileService
    {
        Task UploadAsync(IFormFile file);
        Task RemoveAsync(string filename);
        Task GetAsync(string filename);
    }
}
