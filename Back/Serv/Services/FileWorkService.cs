using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Logging;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Transforms;
using SixLabors.Primitives;

namespace Vk_server
{
    public class FileWorkService : IFileWorkService
    {
        private readonly IHostingEnvironment _environment;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<FileWorkService> _logger;


        public FileWorkService(IHostingEnvironment environment,
            IUnitOfWork uow,
            ILogger<FileWorkService> logger)
        {
            _environment = environment;
            _uow = uow;
            _logger = logger;
        }

        public string PhotoSave(IFormFile file)
        {
            if (file == null || string.IsNullOrEmpty(_environment.WebRootPath))
                return null;

            _logger.LogDebug($"PhotoSave action for file {file.FileName} started!");

            _logger.LogDebug("Check for image.");
            Image<Rgba32> image = Image.Load(file.OpenReadStream());

            _logger.LogDebug("Compute hash from image.");
            string hash = GetHashFromFile(file.OpenReadStream());

            _logger.LogDebug("Compute new image location.");
            string dir1 = _environment.WebRootPath + "/Files/" + hash.Substring(0, 2);
            string dir2 = $"{dir1}/{hash.Substring(2, 2)}/";

            _logger.LogDebug("Check directory existance.");
            if (!Directory.Exists(dir1))
            {
                Directory.CreateDirectory(dir1);
                Directory.CreateDirectory(dir2);
            }
            else if (!Directory.Exists(dir2))
                Directory.CreateDirectory(dir2);

            _logger.LogDebug("Start copy image to server.");
            string result = dir2 + file.FileName;
            image.Save(result);
            _logger.LogDebug($"File \"{file.FileName}\" save to path \"${result}\".");

            _logger.LogDebug("End of PhotoSave action.");
            return result.Replace(_environment.WebRootPath, "");
        }

        private string GetHashFromFile(Stream stream)
        {
            var hash = SHA1.Create().ComputeHash(stream);
            var sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}