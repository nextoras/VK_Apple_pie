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
    public class PythonServerController : Controller
    {
        private IAuthorizationService _authorizationService;
        private IPythonServerService _pythonServerService;

        public PythonServerController(
            IAuthorizationService authorizationService,
            IPythonServerService pythonServerService
            )
        {
            _authorizationService = authorizationService;
            _pythonServerService = pythonServerService;
        }

        /// <summary>
        /// GetSizes
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPost("sizes")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSizes([FromBody] ReadySizesBindingModel model)
        {
            await _pythonServerService.SaveSizesAsync(model);

            return Ok();
        }

        /// <summary>
        /// SaveRenderPhoto
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPost("pushRednderPhoto")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SaveRenderPhoto( RenderPhotoBindingModel model)
        {
            await _pythonServerService.SaveRenderPhotoAsync(model.Photo, model.ClothingId, model.UserId);

            return Ok();
        }
    }
} 