using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Vk_server
{
    public interface IPythonServerService
    {
        Task SendPhotosForSizesAsync(IFormFile photo1, IFormFile photo2);

        Task SaveSizesAsync(ReadySizesBindingModel model);
    }
}