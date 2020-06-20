using System.Collections.Generic;
using System.Threading.Tasks;


namespace Vk_server
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizeAsync(long user_id, string firstName, string lastName, long sexId);
    }
}