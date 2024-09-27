using DevaLokaVillaAPI.Models;
using DevaLokaVillaAPI.Models.Dto;
namespace DevaLokaVillaAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser (string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
