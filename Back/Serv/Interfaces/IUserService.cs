using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Vk_server
{
    public interface IUserService
    {
        Task <bool> PushPhotoAsync(IFormFile photo1, IFormFile photo2, long userId);
        Task<UserDTO> GetUserInfoAsync(long userId);
    }
}