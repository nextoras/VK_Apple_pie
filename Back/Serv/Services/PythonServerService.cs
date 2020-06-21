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
                UserId = userId,
                Height = user.Height
            };
            var uri = "https://6923ece9f762.ngrok.io/sizes/";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await client.PostAsJsonAsync("", requestBody);
        }

        public async Task SaveSizesAsync(ReadySizesBindingModel model)
        {
            var userSizes = await _uow.UserParameters.Query()
                .Where(us => us.UserId == model.userId).FirstOrDefaultAsync();

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

                //определение размера по данным
                
                //верх
                if (model.chest < 89) userSizes.SizeUpId = 1;
                if (model.chest >= 89 && model.chest < 93) userSizes.SizeUpId = 2;
                if (model.chest >= 93 && model.chest < 97) userSizes.SizeUpId = 3;
                if (model.chest >= 97 && model.chest < 102) userSizes.SizeUpId = 4;

                //торс
                if (model.waist < 72) userSizes.SizeMiddleId = 5;
                if (model.waist >= 72 && model.waist < 75) userSizes.SizeMiddleId = 6;
                if (model.waist >= 75 && model.waist < 77.5) userSizes.SizeMiddleId = 7;
                if (model.waist >= 77.5 && model.waist < 80) userSizes.SizeMiddleId = 8;
                if (model.waist >= 80 && model.waist < 82.5) userSizes.SizeMiddleId = 9;
                if (model.waist >= 82.5 && model.waist < 85) userSizes.SizeMiddleId = 10;
                if (model.waist >= 85 && model.waist < 87.5) userSizes.SizeMiddleId = 11;
                if (model.waist >= 87.5 && model.waist < 92.5) userSizes.SizeMiddleId = 12;
                if (model.waist >= 92.5 && model.waist < 100) userSizes.SizeMiddleId = 13;

                //низ
                if (model.leg < 77) userSizes.SizeDownId = 14;
                if (model.waist >= 77 && model.waist < 82) userSizes.SizeMiddleId = 15;
                if (model.waist >= 82 && model.waist < 87) userSizes.SizeMiddleId = 16;
                

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