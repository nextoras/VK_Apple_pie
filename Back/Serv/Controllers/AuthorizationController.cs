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
        /// Create user
        /// </summary>
        /// <param name="user_id">message</param>
        /// <param name="firstName">event identifier</param>
        /// <param name="lastName">distance identifier</param>
        /// <param name="sexId">state</param>
        /// <returns></returns>
        [HttpPost("init")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Authorization(long user_id, string firstName, string lastName, long sexId)
        {
            var result = await _authorizationService.AuthorizeAsync( user_id, firstName, lastName, sexId );

            return Ok(result);
        }

        
    }
} 