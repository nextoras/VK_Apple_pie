using System.Collections.Generic;
using System.Threading.Tasks;


namespace Vk_server
{
    public interface IShopService
    {
        Task <ShopDTO> GetAllAsync(long sex);
        Task <ClotheDTOFull> GetClothingAsync(long clothingId, long userId);
        Task <string> GetRenderPhotoAsync(long clothingId, long userId);
    }
}