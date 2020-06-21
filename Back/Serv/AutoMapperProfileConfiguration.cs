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

            CreateMap<ReadySizesBindingModel, UserParameter>();
            CreateMap<UserParameter, ReadySizesBindingModel>();
            CreateMap<Clothing, ClotheDTO>();
            CreateMap<ClotheDTO, Clothing>();

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.UserParameter, func => func.MapFrom(src => src.Sizes));
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Sizes, func => func.MapFrom(src => src.SizeId)); ;

            CreateMap<Clothing, ClotheDTOFull>();
            CreateMap<ClotheDTOFull, Clothing>();

            CreateMap<SizeDTO, Size>();
            CreateMap<Size, SizeDTO>();
        }

        protected AutoMapperProfileConfiguration(string profileName)
        : base(profileName)
        {
        }
    }
}

