using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PheasantBench.Application.Abstractions;
using PheasantBench.Application.Responses;
using System.IO.Compression;

namespace PheasantBench.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly string _UploadsDirectory;
        private readonly IList<string> _SupportedImageMimeTypes;
        public FileService()
        {
            _SupportedImageMimeTypes = new List<string>() { "image/webp", "image/png", "image/jpg", "image/jpeg" };

            _UploadsDirectory = "wwwroot/uploads";

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
                    response.ErrorMessage = string.Empty;

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
                response.ErrorMessage = string.Empty;

                return response;
            }

            return response;
        }

        public async Task<DataResponse<string>> UploadAsync(IFormFile file)
        {
            DataResponse<string> response = new DataResponse<string>();
            response.Success = false;
            response.ErrorMessage = "Invalid path";

            string fileName = Path.GetFileName(file.FileName);
            string fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

            string filePath = $"{_UploadsDirectory}/{fileName}";

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
            response.ErrorMessage = string.Empty;
            response.Data = fileName;

            return response;
        }

        public async Task<DataResponse<FileContentResult>> DownloadBenchmark(string token)
        {
            string solutionDir = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()) + "/PheasantBenchmark");
            string binDir = Path.Combine(solutionDir, "PheasantBench.App", "bin", "Debug", "net8.0-windows");
            string textFilePath = Path.Combine(binDir, "token.txt");
            string zipFilePath = Path.Combine(binDir, "PheasantBench.zip");

            var response = new DataResponse<FileContentResult>
            {
                Success = false,
                ErrorMessage = "Invalid path"
            };

            try
            {
                if (!Directory.Exists(binDir))
                {
                    Directory.CreateDirectory(binDir);
                }

                string fileContent = token;
                await File.WriteAllTextAsync(textFilePath, fileContent);

                if (File.Exists(zipFilePath))
                {
                    File.Delete(zipFilePath);
                }

                using (FileStream zipToOpen = new FileStream(zipFilePath, FileMode.Create))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                    {
                        archive.CreateEntryFromFile(textFilePath, "token.txt");

                        foreach (var filePath in Directory.GetFiles(binDir))
                        {
                            if (filePath != zipFilePath && filePath != textFilePath)
                            {
                                archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                            }
                        }
                    }
                }

                if (File.Exists(zipFilePath))
                {
                    string contentType = "application/zip";
                    byte[] fileBytes = await File.ReadAllBytesAsync(zipFilePath);

                    response.Data = new FileContentResult(fileBytes, contentType)
                    {
                        FileDownloadName = "PheasantBench.zip"
                    };

                    response.Success = true;
                    response.ErrorMessage = string.Empty;
                }
                else
                {
                    response.ErrorMessage = "Failed to create the zip file.";
                }

                return response;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = $"An error occurred: {ex.Message}";
                return response;
            }
        }
    }
}
