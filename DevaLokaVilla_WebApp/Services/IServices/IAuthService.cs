using DevaLokaVilla_WebApp.Models.Dto;

namespace DevaLokaVilla_WebApp.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO objToCreate);
        Task<T> RegisterAsync<T>(RegistrationRequestDTO objToCreate);


    }
}
