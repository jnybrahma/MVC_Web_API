using AutoMapper;
using DevaLokaVilla_WebApp.Models;
using DevaLokaVilla_WebApp.Models.Dto;

namespace DevaLokaVilla_WebApp
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {

            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
          
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
        }

    }
}
