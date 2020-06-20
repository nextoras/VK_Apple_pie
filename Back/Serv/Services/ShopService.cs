using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Newtonsoft.Json;
using System.Drawing;



namespace Vk_server
{
    public class ShopService : IShopService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPythonServerService _pythonServerService;


        public ShopService(
            IUnitOfWork uow,
            IMapper mapper,
            IPythonServerService pythonServerService,
            ILoggerFactory loggerFactory)
        {
            _uow = uow;
            _mapper = mapper;
            _pythonServerService = pythonServerService;
        }

        public async Task<ShopDTO> GetAllAsync(long sexId)
        {
            var clotheCollection = await _uow.Clothings.Query()
                .Where(c => c.SexId == sexId).ToListAsync();

            ShopDTO clotheCollectionDTO = new ShopDTO();
            List<ClotheDTO> clotheDTOs = new List<ClotheDTO>();

            foreach (var item in clotheCollection)
            {
                var clotheDTO = _mapper.Map<ClotheDTO>(item);
                clotheDTOs.Add(clotheDTO);
            }

            clotheCollectionDTO.Clothes = clotheDTOs;
            return clotheCollectionDTO;
        }


        public async Task<ClotheDTOFull> GetClothingAsync(long clothingId, long userId)
        {
            var clothe = await _uow.Clothings.GetByIdAsync(clothingId);

            if (clothe == null)
            {
                throw new Exception("Одёжа не найдена");
            }

            var result = _mapper.Map<ClotheDTOFull>(clothe);

            result.SizeDTOs = new List<SizeDTO>();

            var clothingSizes = await _uow.ClothingSizes.Query().Where(o => o.ClothingId == clothingId).ToListAsync();

            if (clothingSizes != null)
            {
                foreach (var clothingSize in clothingSizes)
                {
                    var size = await _uow.Sizes.GetByIdAsync(clothingSize.SizeId);
                    SizeDTO sizeDTO = _mapper.Map<SizeDTO>(size);
                    result.SizeDTOs.Add(sizeDTO);
                }
            }

            await _pythonServerService.SendPhotosForSizesAsync(clothingId, userId);
            return result;
        }


        public async Task<string> GetRenderPhotoAsync(long clothingId, long userId)
        {
            var user = await _uow.Users.Query()
                .Where(n => n.VkId == userId).FirstOrDefaultAsync();

            if (user == null) throw new Exception("Пользователь не найден");

            var renderPhoto = await _uow.RenderPhotos.Query()
                .Where(b => b.ClothingId == clothingId && b.UserId == userId).FirstOrDefaultAsync();

            if (renderPhoto != null) return renderPhoto.PhotoPath;
            return "";
        }
        
    }

}