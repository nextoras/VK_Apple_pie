using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using ValidationException = Timerman.Service.Infrastructure.ValidationException;

namespace Vk_server
{
    [Route("api/")]
    public class UserController : Controller
    {
        private IAuthorizationService _authorizationService;
        private IUserService _userService;

        public UserController(
            IAuthorizationService authorizationService,
            IUserService userService
            )
        {
            _authorizationService = authorizationService;
            _userService = userService;
        }

        /// <summary>
        /// load user photo
        /// </summary>
        /// <param name="model">binding model</param>
        /// <returns></returns>
        [HttpPost("load_photo")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutPhoto(UserPhotosBindingModel model)
        {
            var result = await _userService.PushPhotoAsync(model.PhotoFront, model.PhotoSide, model.UserId);

            return Ok();
        }
        
        /// <summary>
        /// get user info
        /// </summary>
        /// <param name="userId">binding model</param>
        /// <returns></returns>
        [HttpGet("get_user")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserInfo(long userId)
        {
            var result = await _userService.GetUserInfoAsync(userId);

            return Ok(result);
        }
    }
} 