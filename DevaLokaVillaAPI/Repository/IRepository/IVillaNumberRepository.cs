using DevaLokaVillaAPI.Models;
using System.Linq.Expressions;

namespace DevaLokaVillaAPI.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
     
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
     

    }
}
