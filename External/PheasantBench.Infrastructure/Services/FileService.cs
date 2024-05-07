using Microsoft.AspNetCore.Http;
using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Responses;

namespace PheasantBench.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public Task<Response> GetAsync(string filename)
        {
            throw new NotImplementedException();
        }

        public Task<Response> RemoveAsync(string filename)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UploadAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
