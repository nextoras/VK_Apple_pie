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



namespace Vk_server
{
    public class ShopService : IShopService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        

        public ShopService(
            IUnitOfWork uow,
            IMapper mapper,
            ILoggerFactory loggerFactory)
        {
            _uow = uow;
            _mapper = mapper;
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


        public async Task<ClotheDTO> GetClothingAsync(long clotheId)
        {
            var clothe = await _uow.Clothings.GetByIdAsync(clotheId);

            if (clothe == null)
            {
                throw new Exception("Одёжа не найдена");
            }
            
            return _mapper.Map<ClotheDTO>(clothe);;
        }
    }

}