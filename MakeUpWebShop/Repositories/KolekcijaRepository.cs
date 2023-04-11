using MakeupWebShop.Db;
using Microsoft.EntityFrameworkCore;

namespace MakeupWebShop.Repositories
{
    public class KolekcijaRepository : IKolekcijaRepository
    {
        private readonly MakeUpDbContext makeUpDbContext;

        public KolekcijaRepository(MakeUpDbContext makeUpDbContext)
        {
            this.makeUpDbContext = makeUpDbContext;
        }
        public async Task<TblKolekcija> AddAsync(TblKolekcija tblKolekcija)
        {
            await makeUpDbContext.AddAsync(tblKolekcija);
            await makeUpDbContext.SaveChangesAsync();
            return tblKolekcija;
        }

        public async Task<TblKolekcija> DeleteAsync(int id)
        {
            var tblKolekcija = await makeUpDbContext.TblKolekcijas.FindAsync(id);
            if (tblKolekcija == null)
            {
                return null;
            }
            makeUpDbContext.TblKolekcijas.Remove(tblKolekcija);
            await makeUpDbContext.SaveChangesAsync();
            return tblKolekcija;
        }

        public async Task<IEnumerable<TblKolekcija>> GetAllAsync()
        {
            return await makeUpDbContext.TblKolekcijas.ToListAsync();
        }

        public async Task<TblKolekcija> GetByIdAsync(int id)
        {
            return await makeUpDbContext.TblKolekcijas.FirstOrDefaultAsync(x => x.KolekcijaId == id);
        }

        public async Task<TblKolekcija> UpdateAsync(int id, TblKolekcija tblKolekcija)
        {
            var existingTblKolekcija = await makeUpDbContext.TblKolekcijas.FirstOrDefaultAsync(x => x.KolekcijaId == id);
            if (existingTblKolekcija == null)
            {
                return null;
            }
            existingTblKolekcija.NazivKolekcije = tblKolekcija.NazivKolekcije;

            await makeUpDbContext.SaveChangesAsync();

            return existingTblKolekcija;
        }
    }
}
