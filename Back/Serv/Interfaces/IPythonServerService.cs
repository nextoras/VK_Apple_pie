using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Vk_server
{
    public interface IPythonServerService
    {
        Task SendPhotosForSizesAsync(long clothingId, long userId);

        Task SaveSizesAsync(ReadySizesBindingModel model);

        Task SaveRenderPhotoAsync(IFormFile photo, long clothingId, long userId);
    }
}