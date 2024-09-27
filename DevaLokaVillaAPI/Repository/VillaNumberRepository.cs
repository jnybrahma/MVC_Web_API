using DevaLokaVillaAPI.Data;
using DevaLokaVillaAPI.Models;
using DevaLokaVillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DevaLokaVillaAPI.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {

        private readonly ApplicationDbContext _context;

        public VillaNumberRepository(ApplicationDbContext context): base (context) 
        {
            _context = context;

        }    
      
        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {

            entity.UpdatedDate = DateTime.Now;
            _context.VillaNumbers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

     
    }
}
