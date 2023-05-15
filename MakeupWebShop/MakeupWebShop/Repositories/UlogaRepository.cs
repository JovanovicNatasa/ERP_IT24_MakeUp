using MakeupWebShop.Db;
using Microsoft.EntityFrameworkCore;

namespace MakeupWebShop.Repositories
{
    public class UlogaRepository : IUlogaRepository
    {
        private readonly MakeUpDbContext makeUpDbContext;

        public UlogaRepository(MakeUpDbContext makeUpDbContext)
        {
            this.makeUpDbContext = makeUpDbContext;
        }
        public async Task<TblUloga> AddAsync(TblUloga tblUloga)
        {
            await makeUpDbContext.AddAsync(tblUloga);
            await makeUpDbContext.SaveChangesAsync();
            return tblUloga;
        }

        public async Task<TblUloga> DeleteAsync(int id)
        {
            var tblUloga = await makeUpDbContext.TblUlogas.FindAsync(id);
            if (tblUloga == null)
            {
                return null;
            }
            makeUpDbContext.TblUlogas.Remove(tblUloga);
            await makeUpDbContext.SaveChangesAsync();
            return tblUloga;
        }

        public async Task<IEnumerable<TblUloga>> GetAllAsync()
        {
            return await makeUpDbContext.TblUlogas.ToListAsync();
        }

        public async Task<TblUloga> GetByIdAsync(int id)
        {
            return await makeUpDbContext.TblUlogas.FirstOrDefaultAsync(x => x.UlogaId == id);
        }

        public async Task<TblUloga> UpdateAsync(int id, TblUloga tblUloga)
        {
            var existingTblUloga = await makeUpDbContext.TblUlogas.FirstOrDefaultAsync(x => x.UlogaId == id);
            if (existingTblUloga == null)
            {
                return null;
            }
            existingTblUloga.NazivUloge = tblUloga.NazivUloge;

            await makeUpDbContext.SaveChangesAsync();

            return existingTblUloga;
        }
    }
}
