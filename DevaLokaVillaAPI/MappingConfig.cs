using AutoMapper;
using DevaLokaVillaAPI.Models;
using DevaLokaVillaAPI.Models.Dto;

namespace DevaLokaVillaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {

            CreateMap<Villa, VillaDTO>();
            CreateMap<Villa, Villa>();
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();

        }

    }
}
