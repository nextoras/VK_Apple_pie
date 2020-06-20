using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using AutoMapper;



namespace Vk_server
{
    public class PythonServerService : IPythonServerService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PythonServerService(
            IUnitOfWork uow,
            IMapper mapper,
            ILoggerFactory loggerFactory)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<string> AuthorizeAsync()
        {
            var result = await _uow.Shops.Query().FirstOrDefaultAsync();
            return result.About;
        }

        public async Task SendPhotosForSizesAsync(IFormFile photo1, IFormFile photo2)
        {
            SizePhotosBindingModel requestBody = new SizePhotosBindingModel
            {
                PhotoFront = photo1,
                PhotoSide = photo2
            };
            var uri ="";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await client.PostAsJsonAsync("", requestBody);
        }

        public async Task SaveSizesAsync(ReadySizesBindingModel model)
        {
            var userSizes = await _uow.Sizes.Query()
                .Where(us => us.UserId == model.UserId).FirstOrDefaultAsync();

            if (userSizes != null)
            {
                var id = userSizes.Id;
                _mapper.Map<ReadySizesBindingModel, Size>(model, userSizes);
                userSizes.Id = id;
                _uow.Sizes.Update(userSizes);
            } 
            else
            {
                userSizes = _mapper.Map<Size>(model);

                await _uow.Sizes.CreateRAsync(userSizes);
            }

            await _uow.SaveChangesAsync();
        }
    }

}