using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using AutoMapper;


namespace Vk_server
{
    public class AuthorizationService : IAuthorizationService
    {

        private readonly IUnitOfWork _uow;

        public AuthorizationService(
            IUnitOfWork uow,
            ILoggerFactory loggerFactory)
        {
            _uow = uow;
        }

        public async Task AuthorizeAsync(long user_id, string firstName, string lastName, long sexId)
        {
            var user = await _uow.Users.Query()
                .Where(us => us.VkId == user_id).FirstOrDefaultAsync();

            if (user == null)
            {
                
                var userItem = await _uow.Users.CreateRAsync(new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    SexId = sexId,
                    VkId = user_id
                });

                await _uow.SaveChangesAsync();
            }
        }


    }

}