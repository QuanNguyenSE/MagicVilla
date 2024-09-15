using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Repository.IRepository;

namespace MagicVilla.API.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Villa> UpdateAsync(Villa entity)
        {
            _db.Villas.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
