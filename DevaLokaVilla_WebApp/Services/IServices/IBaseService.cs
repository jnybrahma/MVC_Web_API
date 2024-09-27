using DevaLokaVilla_WebApp.Models;

namespace DevaLokaVilla_WebApp.Services.IServices
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }

        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
