using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vk_server
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        : this("MyProfile")
        {

            CreateMap<ReadySizesBindingModel, Size>();
            CreateMap<Size, ReadySizesBindingModel>();
            CreateMap<Clothing, ClotheDTO>();
            CreateMap<ClotheDTO, Clothing>();
        }

        protected AutoMapperProfileConfiguration(string profileName)
        : base(profileName)
        {
        }
    }
}

