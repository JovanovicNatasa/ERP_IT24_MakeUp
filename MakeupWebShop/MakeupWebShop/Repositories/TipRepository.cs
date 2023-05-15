using MakeupWebShop.Db;
using Microsoft.EntityFrameworkCore;

namespace MakeupWebShop.Repositories
{
    public class TipRepository : ITipRepository
    {
        private readonly MakeUpDbContext makeUpDbContext;

        public TipRepository(MakeUpDbContext makeUpDbContext)
        {
            this.makeUpDbContext = makeUpDbContext;
        }
        public async Task<TblTip> AddAsync(TblTip tblTip)
        {
            await makeUpDbContext.AddAsync(tblTip);
            await makeUpDbContext.SaveChangesAsync();
            return tblTip;
        }

        public async Task<TblTip> DeleteAsync(int id)
        {
            var tblTip = await makeUpDbContext.TblTips.FindAsync(id);
            if (tblTip == null)
            {
                return null;
            }
            makeUpDbContext.TblTips.Remove(tblTip);
            await makeUpDbContext.SaveChangesAsync();
            return tblTip;
        }

        public async Task<IEnumerable<TblTip>> GetAllAsync()
        {
            return await makeUpDbContext.TblTips.ToListAsync();
        }

        public async Task<TblTip> GetByIdAsync(int id)
        {
            return await makeUpDbContext.TblTips.FirstOrDefaultAsync(x => x.TipId == id);
        }

        public async Task<TblTip> UpdateAsync(int id, TblTip tblTip)
        {
            var existingTblTip = await makeUpDbContext.TblTips.FirstOrDefaultAsync(x => x.TipId == id);
            if (existingTblTip == null)
            {
                return null;
            }
            existingTblTip.NazivTipa = existingTblTip.NazivTipa;

            await makeUpDbContext.SaveChangesAsync();

            return existingTblTip;
        }
    }
}
