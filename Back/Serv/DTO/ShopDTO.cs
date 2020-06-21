using System;
using System.Collections.Generic;
using System.Text;

namespace Vk_server
{
    public class ShopDTO
    {
        public List<ClotheDTO> Clothes { get; set; }
    }

    public class ClotheDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long SexId { get; set; }
        public string Picture { get; set; }
        public string About { get; set; }
        public decimal Price { get; set; }
        public long? ClothingSizeId { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public long ShopId { get; set; }
    }

    public class ClotheDTOFull : ClotheDTO
    {
        public List<SizeDTO> SizeDTOs { get; set; }
    }

    public class SizeDTO
    {
        public long id { get; set;}
        public string SizeName { get; set;}
        public  double? SizeN { get; set; }
    }
}