using AutoMapper;
using MagicVilla.Web.Models.Dto;

namespace MagicVilla.Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {


            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();

        }
    }
}
