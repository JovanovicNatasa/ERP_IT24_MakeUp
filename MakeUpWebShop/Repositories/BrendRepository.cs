using MakeupWebShop.Db;
using Microsoft.EntityFrameworkCore;

namespace MakeupWebShop.Repositories
{
    public class BrendRepository : IBrendRepository
    {
        private readonly MakeUpDbContext makeUpDbContext;

        public BrendRepository(MakeUpDbContext makeUpDbContext)
        {
            this.makeUpDbContext = makeUpDbContext;
        }

        public async Task<TblBrend> AddAsync(TblBrend tblBrend)
        {
            await makeUpDbContext.AddAsync(tblBrend);
            await makeUpDbContext.SaveChangesAsync();
            return tblBrend;
        }

        public async Task<TblBrend> DeleteAsync(int id)
        {
            var tblBrend = await makeUpDbContext.TblBrends.FindAsync(id);
            if (tblBrend == null)
            {
                return null;
            }
            makeUpDbContext.TblBrends.Remove(tblBrend);
            await makeUpDbContext.SaveChangesAsync();
            return tblBrend;
        }

        public async Task<IEnumerable<TblBrend>> GetAllAsync()
        {
            return await makeUpDbContext.TblBrends.ToListAsync();
        }

        public async Task<TblBrend> GetByIdAsync(int id)
        {
            return await makeUpDbContext.TblBrends.FirstOrDefaultAsync(x => x.BrendId == id);
        }

        public async Task<TblBrend> UpdateAsync(int id, TblBrend tblBrend)
        {
            var existingTblBrend = await makeUpDbContext.TblBrends.FirstOrDefaultAsync(x => x.BrendId == id);
            if (existingTblBrend == null)
            {
                return null;
            }
            existingTblBrend.NazivBrenda= tblBrend.NazivBrenda;

            await makeUpDbContext.SaveChangesAsync();

            return existingTblBrend;
        }
    }
}
