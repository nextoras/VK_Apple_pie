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
    public class AuthorizationController : Controller
    {
        private IAuthorizationService _authorizationService;

        public AuthorizationController(
            IAuthorizationService authorizationService
            )
        {
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Returns info about all events
        /// </summary>
        /// <param name="access_token">message</param>
        /// <param name="expires_in">event identifier</param>
        /// <param name="user_id">distance identifier</param>
        /// <param name="state">state</param>
        /// <returns></returns>
        [HttpPost("init")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Authorization(long user_id, string firstName, string lastName, long sexId)
        {
            await _authorizationService.AuthorizeAsync( user_id, firstName, lastName, sexId );

            return Ok();
        }

        
    }
} 