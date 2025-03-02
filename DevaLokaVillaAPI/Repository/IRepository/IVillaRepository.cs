﻿using DevaLokaVillaAPI.Models;
using System.Linq.Expressions;

namespace DevaLokaVillaAPI.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
      
         // Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null);
        // Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool tracked= true);

        //Task CreateAsync(Villa entity);

        Task<Villa> UpdateAsync(Villa entity);
        //Task RemoveAsync(Villa entity);
        //Task SaveAsync();


    }
}
