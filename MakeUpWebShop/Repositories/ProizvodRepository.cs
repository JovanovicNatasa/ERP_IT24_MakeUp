using MakeupWebShop.Db;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace MakeupWebShop.Repositories
{
    public class ProizvodRepository : IProizvodRepository
    {
        private readonly MakeUpDbContext makeUpDbContext;

        public ProizvodRepository(MakeUpDbContext makeUpDbContext)
        {
            this.makeUpDbContext = makeUpDbContext;
        }
        public async Task<TblProizvod> AddAsync(TblProizvod tblProizvod)
        {
            await makeUpDbContext.AddAsync(tblProizvod);
            await makeUpDbContext.SaveChangesAsync();
            return tblProizvod;
        }

        public async Task<TblProizvod> DeleteAsync(int id)
        {
            var tblProizvod = await makeUpDbContext.TblProizvods.FindAsync(id);
            if (tblProizvod == null)
            {
                return null;
            }
            makeUpDbContext.TblProizvods.Remove(tblProizvod);
            await makeUpDbContext.SaveChangesAsync();
            return tblProizvod;
        }

        public async Task<IEnumerable<TblProizvod>> GetAllAsync()
        {
            return await makeUpDbContext.TblProizvods.Include(x => x.Brend).Include(x => x.Tip)
                .Include(x => x.Namena).Include(x => x.Kolekcija).ToListAsync();
        }

        public async Task<TblProizvod> GetByIdAsync(int id)
        {
            return await makeUpDbContext.TblProizvods.Include(x => x.Brend).Include(x => x.Tip)
                .Include(x => x.Namena).Include(x => x.Kolekcija)
                .FirstOrDefaultAsync(x => x.ProizvodId == id);
        }

        public async Task<TblProizvod> UpdateAsync(int id, TblProizvod tblProizvod)
        {
            var existingTblProizvod = await makeUpDbContext.TblProizvods.FirstOrDefaultAsync(x => x.ProizvodId == id);
            if (existingTblProizvod == null)
            {
                return null;
            }
            
            existingTblProizvod.Model = tblProizvod.Model;
            existingTblProizvod.Sastav = tblProizvod.Sastav;
            existingTblProizvod.NacinUpotrebe = tblProizvod.NacinUpotrebe;
            existingTblProizvod.CenaPoKom = tblProizvod.CenaPoKom;
            existingTblProizvod.KolicinaNaStanju = tblProizvod.KolicinaNaStanju;
            existingTblProizvod.BrendId = tblProizvod.BrendId;
            existingTblProizvod.TipId = tblProizvod.TipId;
            existingTblProizvod.NamenaId = tblProizvod.NamenaId;
            existingTblProizvod.KolekcijaId = tblProizvod.KolekcijaId;

            await makeUpDbContext.SaveChangesAsync();

            return existingTblProizvod;
        }
      
    }
}
