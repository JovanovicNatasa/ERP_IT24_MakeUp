using MakeupWebShop.Db;
using Microsoft.EntityFrameworkCore;

namespace MakeupWebShop.Repositories
{
    public class NamenaRepository : INamenaRepository
    {
        private readonly MakeUpDbContext makeUpDbContext;

        public NamenaRepository(MakeUpDbContext makeUpDbContext)
        {
            this.makeUpDbContext = makeUpDbContext;
        }

        public async Task<TblNamena> AddAsync(TblNamena tblNamena)
        {
            await makeUpDbContext.AddAsync(tblNamena);
            await makeUpDbContext.SaveChangesAsync();
            return tblNamena;
        }

        public async Task<TblNamena> DeleteAsync(int id)
        {
            var tblNamena = await makeUpDbContext.TblNamenas.FindAsync(id);
            if (tblNamena == null)
            {
                return null;
            }
            makeUpDbContext.TblNamenas.Remove(tblNamena);
            await makeUpDbContext.SaveChangesAsync();
            return tblNamena;
        }

        public async Task<IEnumerable<TblNamena>> GetAllAsync()
        {
            return await makeUpDbContext.TblNamenas.ToListAsync();
        }

        public async Task<TblNamena> GetByIdAsync(int id)
        {
            return await makeUpDbContext.TblNamenas.FirstOrDefaultAsync(x => x.NamenaId == id);
        }

        public async Task<TblNamena> UpdateAsync(int id, TblNamena tblNamena)
        {
            var existingTblNamena = await makeUpDbContext.TblNamenas.FirstOrDefaultAsync(x => x.NamenaId == id);
            if (existingTblNamena == null)
            {
                return null;
            }
            existingTblNamena.NazivNamene = tblNamena.NazivNamene;

            await makeUpDbContext.SaveChangesAsync();

            return existingTblNamena;
        }
    }
}
