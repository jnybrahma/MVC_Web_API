﻿using DevaLokaVilla_WebApp.Models.Dto;

namespace DevaLokaVilla_WebApp.Services.IServices
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(VillaUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
