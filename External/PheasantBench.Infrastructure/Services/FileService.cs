using Microsoft.AspNetCore.Http;
using PheasantBench.Application.Abstractions;

namespace PheasantBench.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public Task GetAsync(string filename)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string filename)
        {
            throw new NotImplementedException();
        }

        public Task UploadAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
