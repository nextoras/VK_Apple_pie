using System.Collections.Generic;
using System.Threading.Tasks;


namespace Vk_server
{
    public interface IShopService
    {
        Task <ShopDTO> GetAllAsync(long sex);
        Task <ClotheDTO> GetClothingAsync(long clothingId);
    }
}