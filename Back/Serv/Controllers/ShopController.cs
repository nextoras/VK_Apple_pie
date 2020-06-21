using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Vk_server
{
    [Route("api/")]
    public class ShopController : Controller
    {
        private IAuthorizationService _authorizationService;
        private IShopService _shopService;

        public ShopController(
            IAuthorizationService authorizationService,
            IShopService shopService
            )
        {
            _authorizationService = authorizationService;
            _shopService = shopService;
        }

        /// <summary>
        /// Returns info about all events
        /// </summary>
        /// <param name="sexId">sex</param>
        /// <returns></returns>
        [HttpGet("catalog")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll(long sexId)
        {
            var result = await _shopService.GetAllAsync(sexId);

            return Ok(result);
        }

        /// <summary>
        /// get clothing
        /// </summary>
        /// <param name="clothingId">sex</param>
        /// <param name="userId">sex</param>
        /// <returns></returns>
        [HttpGet("clothing")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetClothing(long clothingId, long userId)
        {
            var result = await _shopService.GetClothingAsync(clothingId,  userId);

            return Ok(result);
        }

        /// <summary>
        ///  Get render photo
        /// </summary>
        /// <param name="clothingId">sex</param>
        /// <param name="userId">sex</param>
        /// <returns></returns>
        [HttpGet("renderPhoto")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRenderPhoto(long clothingId, long userId)
        {
            var result = await _shopService.GetRenderPhotoAsync(clothingId,  userId);

            return Ok(result);
        }

    }
} 