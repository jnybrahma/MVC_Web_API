using DevaLokaVillaAPI.Data;
using DevaLokaVillaAPI.Models;
using DevaLokaVillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DevaLokaVillaAPI.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {

        private readonly ApplicationDbContext _context;

        public VillaRepository(ApplicationDbContext context): base (context) 
        {
            _context = context;

        }    
        //public async Task CreateAsync(Villa entity)
        //{
        //    await _context.Villas.AddAsync(entity);
        //    await SaveAsync();

        //}

        //public async Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
        //{
        //    IQueryable<Villa> query = _context.Villas;

        //    if(!tracked)
        //    {
        //        query = query.AsNoTracking();
        //    }

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    return await query.FirstOrDefaultAsync();
        //}

        //public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null)
        //{
        //    IQueryable<Villa> query = _context.Villas;
            
        //    if(filter !=null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    return await query.ToListAsync();
        //}

        //public async Task RemoveAsync(Villa entity)
        //{
        //     _context.Villas.Remove(entity);
        //    await SaveAsync();

        //}

        //public async Task SaveAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}

        public async Task<Villa> UpdateAsync(Villa entity)
        {

            entity.UpdatedDate = DateTime.Now;
            _context.Villas.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
