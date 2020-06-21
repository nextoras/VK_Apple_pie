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
using AutoMapper;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;



namespace Vk_server
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _uow;
        private readonly IFileWorkService _fileWorkService;
        private readonly IPythonServerService _pythonServerService;
        private readonly IMapper _mapper;


        public UserService(
            IUnitOfWork uow,
            IFileWorkService fileWorkService,
            IPythonServerService pythonServerService,
            IMapper mapper,
            ILoggerFactory loggerFactory)
        {
            _uow = uow;
            _fileWorkService = fileWorkService;
            _pythonServerService = pythonServerService;
            _mapper = mapper;
        }

        public async Task<string> AuthorizeAsync()
        {
            var result = await _uow.Shops.Query().FirstOrDefaultAsync();
            return result.About;
        }

        public async Task<bool> PushPhotoAsync(IFormFile photo1, IFormFile photo2, long userId, long height)
        {
            var user = await _uow.Users.Query().Where(q => q.VkId == userId).FirstOrDefaultAsync();

            if (user == null) throw new Exception("Юзер не найден");

            user.Height = height;

            var userPhotos = await _uow.PhotoHumans.Query()
                .Where(s => s.UserId == user.Id).ToListAsync();

            if (userPhotos != null)
            {
                foreach (var photo in userPhotos)
                {
                    _uow.PhotoHumans.RemoveById(photo.Id);
                }
                await _uow.SaveChangesAsync();
            }

            var srcPath = "";
            var srcPathSide = "";
            if (photo1 != null)
                srcPath = _fileWorkService.PhotoSave(photo1);

            if (photo2 != null)
                srcPathSide = _fileWorkService.PhotoSave(photo2);

            await _uow.PhotoHumans.CreateRAsync(new PhotoHuman()
            {
                UserId = user.Id,
                PhotoPath = srcPath
            });

            await _uow.PhotoHumans.CreateRAsync(new PhotoHuman()
            {
                UserId = user.Id,
                PhotoPath = srcPathSide
            });

            _uow.Users.Update(user);

            await _uow.SaveChangesAsync();

            try
            {
                Image imageUser = Image.FromFile("wwwroot" + srcPath);
                Image imageClothing = Image.FromFile("wwwroot" + srcPathSide);

                //IFormFile photo1, IFormFile photo2,
                SizePhotosBindingModel requestBody = new SizePhotosBindingModel
                {
                    PhotoFront = imageUser,
                    PhotoSide = imageClothing,
                    UserId = userId,
                    Height = user.Height
                };
                var uri = "https://6923ece9f762.ngrok.io/sizes";

                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(uri);
                var response = await client.PostAsJsonAsync("", requestBody);
            }
            catch (System.Exception)
            {

                throw new Exception("Server isn't reachable ");
            }

            return true;
        }

        public async Task<UserDTO> GetUserInfoAsync(long userId)
        {
            var user = await _uow.Users.Query()
                .Where(aw => aw.VkId == userId).FirstOrDefaultAsync();

            if (user == null) throw new Exception("User не найден");

            UserDTO userDTO = new UserDTO();
            userDTO.UserParameters = new UserParametersDTO();
            UserParametersDTO sizeDTO = new UserParametersDTO();

            var sizes = await _uow.UserParameters.Query()
                .Where(l => l.UserId == user.Id).FirstOrDefaultAsync();

            userDTO = _mapper.Map<UserDTO>(user);

            if (sizes != null)
            {
                sizeDTO = _mapper.Map<UserParametersDTO>(sizes);
                userDTO.UserParameters = sizeDTO;
            }

            return userDTO;

        }

    }

}