using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vk_server
{
    public interface IFileWorkService
    {
        /// <summary>
        /// Save photo 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        string PhotoSave(IFormFile file);
    }
}