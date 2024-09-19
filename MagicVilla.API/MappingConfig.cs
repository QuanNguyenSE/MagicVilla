using AutoMapper;
using MagicVilla.API.Models;
using MagicVilla.API.Models.Dto;

namespace MagicVilla.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();

            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();

            CreateMap<LocalUser, RegisterationRequestDTO>().ReverseMap();
            CreateMap<LocalUser, LoginRequestDTO>().ReverseMap();
        }
    }
}
