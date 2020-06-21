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
using System.Drawing;
using form = SixLabors;
using SixLabors.ImageSharp.PixelFormats;

namespace Vk_server
{
    public class PythonServerService : IPythonServerService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IFileWorkService _fileWorkService;

        public PythonServerService(
            IUnitOfWork uow,
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IFileWorkService fileWorkService)
        {
            _uow = uow;
            _mapper = mapper;
            _fileWorkService = fileWorkService;
        }

        public async Task<string> AuthorizeAsync()
        {
            var result = await _uow.Shops.Query().FirstOrDefaultAsync();
            return result.About;
        }

        public async Task SendPhotosForSizesAsync(long clothingId, long userId)
        {
            var user = await _uow.Users.GetByIdAsync(userId);
            var userPhotoPath = await _uow.PhotoHumans.Query()
                .Where(b => b.UserId == user.Id).FirstOrDefaultAsync();

            var clothingPhoto = await _uow.Clothings.GetByIdAsync(clothingId);

            
            Image imageUser = Image.FromFile("wwwroot" + userPhotoPath.PhotoPath);
            Image imageClothing = Image.FromFile("wwwroot" + clothingPhoto.Picture);

            //IFormFile photo1, IFormFile photo2,
            SizePhotosBindingModel requestBody = new SizePhotosBindingModel
            {
                PhotoFront = imageUser,
                PhotoSide = imageClothing,
                UserId = userId
            };
            var uri = "http://127.0.0.1:5001/";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await client.PostAsJsonAsync("", requestBody);
        }

        public async Task SaveSizesAsync(ReadySizesBindingModel model)
        {
            var userSizes = await _uow.UserParameters.Query()
                .Where(us => us.UserId == model.UserId).FirstOrDefaultAsync();

            if (userSizes != null)
            {
                var id = userSizes.Id;
                _mapper.Map<ReadySizesBindingModel, UserParameter>(model, userSizes);
                userSizes.Id = id;
                _uow.UserParameters.Update(userSizes);
            }
            else
            {
                userSizes = _mapper.Map<UserParameter>(model);

                await _uow.UserParameters.CreateRAsync(userSizes);
            }

            await _uow.SaveChangesAsync();
        }

        public async Task SaveRenderPhotoAsync(IFormFile photo, long clothingId, long userId)
        {
            var photoPath = _fileWorkService.PhotoSave(photo);
            await _uow.RenderPhotos.CreateRAsync(new RenderPhoto()
            {
                PhotoPath = photoPath,
                UserId = userId,
                ClothingId = clothingId
            });
        }
    }

}