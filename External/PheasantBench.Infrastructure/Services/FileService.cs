using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Responses;

namespace PheasantBench.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly string _UploadsDirectory;
        private readonly IList<string> _SupportedImageMimeTypes;
        public FileService()
        {
            _SupportedImageMimeTypes = new List<string>() { "image/webp", "image/png", "image/jpg", "image/jpeg" };

            _UploadsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Media");

            if (!Directory.Exists(_UploadsDirectory))
            {
                Directory.CreateDirectory(_UploadsDirectory);
            }
        }

        public string GetContentType(string fileName)
        {
            if (fileName.EndsWith(".webp", StringComparison.OrdinalIgnoreCase))
            {
                return "image/webp";
            }
            else if (fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            {
                return "image/png";
            }
            else if (fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
            {
                return "image/jpg";
            }
            else
            {
                return "unsupported";
            }
        }

        public async Task<DataResponse<FileContentResult>> GetAsync(string filename)
        {
            string filePath = Path.Combine(_UploadsDirectory, filename);

            DataResponse<FileContentResult> response = new DataResponse<FileContentResult>();
            response.Success = false;
            response.ErrorMessage = "Invalid path";

            try
            {
                if (File.Exists(filePath))
                {
                    string contentType = GetContentType(filename);
                    byte[] fileBytes = await File.ReadAllBytesAsync(filePath);

                    response.Data = new FileContentResult(fileBytes, contentType);
                    response.Success = true;
                    response.ErrorMessage = "";

                    return response;
                }

                return response;
            }
            catch
            {
                return response;
            }
        }

        public async Task<Response> RemoveAsync(string filename)
        {
            Response response = new Response();
            response.Success = false;
            response.ErrorMessage = "Invalid path";

            string filePath = Path.Combine(_UploadsDirectory, filename);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                response.Success = true;
                response.ErrorMessage = "";

                return response;
            }

            return response;
        }

        public async Task<Response> UploadAsync(IFormFile file)
        {
            Response response = new Response();
            response.Success = false;
            response.ErrorMessage = "Invalid path";

            string fileName = Path.GetFileName(file.FileName);
            string fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

            string filePath = Path.Combine(_UploadsDirectory, fileName);

            if (File.Exists(filePath))
            {
                response.ErrorMessage = "File already exists";
                return response;
            }

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            response.Success = true;
            response.ErrorMessage = "";

            return response;
        }
    }
}
