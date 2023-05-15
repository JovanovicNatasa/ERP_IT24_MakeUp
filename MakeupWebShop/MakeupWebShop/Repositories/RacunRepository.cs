using MakeupWebShop.Db;
using MakeupWebShop.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace MakeupWebShop.Repositories
{
    public class RacunRepository : IRacunRepository
    {
        private readonly MakeUpDbContext makeUpDbContext;
        private readonly IKorpaRepository korpaRepository;

        public RacunRepository(MakeUpDbContext makeUpDbContext, IKorpaRepository korpaRepository)
        {
            this.makeUpDbContext = makeUpDbContext;
            this.korpaRepository = korpaRepository;
        }
        public async Task<TblRacun> AddAsync(TblRacun tblRacun)
        {
            var korpa = await korpaRepository.GetByIdAsync(tblRacun.KorpaId);
            var iznos = korpa.UkupanIznos;
            tblRacun.IznosPost = 300;
            if (iznos >= 5000)
            {
                tblRacun.IznosPost = 0;
            }
            tblRacun.IznosSaPost = (decimal)(iznos + tblRacun.IznosPost);

            await makeUpDbContext.AddAsync(tblRacun);
            await makeUpDbContext.SaveChangesAsync();

            // Fetch the updated entity from the context
           // var updatedEntity = await makeUpDbContext.TblRacuns.FindAsync(tblRacun.RacunId);

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

        public async Task<TblRacun> CalculateShipping(int racunId)
        {

            var racun = await makeUpDbContext.TblRacuns.FirstOrDefaultAsync(r => r.RacunId == racunId);

            if (racun.IznosPopusta > 0)
            {
                racun.IznosSaPost = (decimal)(racun.IznosSaPopustom + racun.IznosPost);
                await makeUpDbContext.SaveChangesAsync();

                // Fetch the updated bill again to capture any changes made by triggers
                //racun = await makeUpDbContext.TblRacuns.FirstOrDefaultAsync(r => r.RacunId == racunId);

                return racun;
            }
            else
            {
                return null;
            }

        }
    }
}
