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

        public async Task<bool> PushPhotoAsync(IFormFile photo1, IFormFile photo2, long userId)
        {
            var user = _uow.Users.Query().Where(q => q.VkId == userId).FirstOrDefaultAsync();

            if (user == null) throw new Exception("Юзер не найден");

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

            await _uow.SaveChangesAsync();

            return true;
        }

        public async Task<UserDTO> GetUserInfoAsync(long userId)
        {
            var user = await _uow.Users.Query()
                .Where(aw => aw.VkId == userId).FirstOrDefaultAsync();

            if (user == null) throw new Exception("User не найден");

            UserDTO userDTO = new UserDTO();
            SizeDTO sizeDTO = new SizeDTO();

            var sizes = await _uow.Sizes.Query()
                .Where(l => l.UserId == user.Id).FirstOrDefaultAsync();

            if (sizes != null)
            {
                sizeDTO = _mapper.Map<SizeDTO>(sizes);
                userDTO.Sizes = sizeDTO;
            }

            userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;

        }

    }

}