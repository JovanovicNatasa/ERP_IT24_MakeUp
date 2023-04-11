using MakeupWebShop.Db;
using Microsoft.EntityFrameworkCore;

namespace MakeupWebShop.Repositories
{
    public class RacunRepository : IRacunRepository
    {
        private readonly MakeUpDbContext makeUpDbContext;

        public RacunRepository(MakeUpDbContext makeUpDbContext)
        {
            this.makeUpDbContext = makeUpDbContext;
        }
        public async Task<TblRacun> AddAsync(TblRacun tblRacun)
        {
            await makeUpDbContext.AddAsync(tblRacun);
            await makeUpDbContext.SaveChangesAsync();
            return tblRacun;
        }

        public async Task<TblRacun> DeleteAsync(int id)
        {
            var tblRacun = await makeUpDbContext.TblRacuns.FindAsync(id);
            if (tblRacun == null)
            {
                return null;
            }
            makeUpDbContext.TblRacuns.Remove(tblRacun);
            await makeUpDbContext.SaveChangesAsync();
            return tblRacun;
        }

        public async Task<IEnumerable<TblRacun>> GetAllAsync()
        {
            return await makeUpDbContext.TblRacuns.Include(x => x.Korpa).ToListAsync();
        }

        public async Task<TblRacun> GetByIdAsync(int id)
        {
            return await makeUpDbContext.TblRacuns.Include(x => x.Korpa).FirstOrDefaultAsync(x => x.RacunId == id);
        }

        public async Task<TblRacun> UpdateAsync(int id, TblRacun tblRacun)
        {
            var existingTblRacun = await makeUpDbContext.TblRacuns.FirstOrDefaultAsync(x => x.RacunId == id);
            if (existingTblRacun == null)
            {
                return null;
            }

            existingTblRacun.DatumKupovine = tblRacun.DatumKupovine;
            existingTblRacun.VremeKupovine = tblRacun.VremeKupovine;
            existingTblRacun.IznosPost = tblRacun.IznosPost;
            existingTblRacun.IznosSaPost = tblRacun.IznosSaPost;
            existingTblRacun.IznosPopusta = tblRacun.IznosPopusta;
            existingTblRacun.IznosSaPopustom = tblRacun.IznosSaPopustom;
            existingTblRacun.KorpaId = tblRacun.KorpaId;


            await makeUpDbContext.SaveChangesAsync();

            return existingTblRacun;
        }
    }
}
