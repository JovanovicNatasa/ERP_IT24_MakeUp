using MakeupWebShop.Db;
using MakeupWebShop.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MakeupWebShop.Repositories
{
    public class KorpaRepository : IKorpaRepository
    {
        private readonly MakeUpDbContext makeUpDbContext;


        public KorpaRepository(MakeUpDbContext makeUpDbContext)
        {
            this.makeUpDbContext = makeUpDbContext;
        }
        public async Task<TblKorpa> AddAsync(TblKorpa tblKorpa)
        {
            await makeUpDbContext.AddAsync(tblKorpa);
            await makeUpDbContext.SaveChangesAsync();
            return tblKorpa;
        }

        public async Task<TblKorpa> DeleteAsync(int id)
        {
            var tblKorpa = await makeUpDbContext.TblKorpas.FindAsync(id);
            if (tblKorpa == null)
            {
                return null;
            }
            makeUpDbContext.TblKorpas.Remove(tblKorpa);
            await makeUpDbContext.SaveChangesAsync();
            return tblKorpa;
        }

        public async Task<IEnumerable<TblKorpa>> GetAllAsync()
        {
            return await makeUpDbContext.TblKorpas.Include(x => x.Korisnik).ToListAsync();
        }

        public async Task<TblKorpa> GetByIdAsync(int id)
        {
            return await makeUpDbContext.TblKorpas.Include(x => x.Korisnik).FirstOrDefaultAsync(x => x.KorpaId == id);
        }

        public async Task<TblKorpa> UpdateAsync(int id, TblKorpa tblKorpa)
        {
            var existingTblKorpa = await makeUpDbContext.TblKorpas.FirstOrDefaultAsync(x => x.KorpaId == id);
            if (existingTblKorpa == null)
            {
                return null;
            }

            existingTblKorpa.UkupanIznos = tblKorpa.UkupanIznos;
            existingTblKorpa.BrProizvoda = tblKorpa.BrProizvoda;
            existingTblKorpa.Popust = tblKorpa.Popust;
            existingTblKorpa.ProcenatPop = tblKorpa.ProcenatPop;
            existingTblKorpa.KorisnikId = tblKorpa.KorisnikId;


            await makeUpDbContext.SaveChangesAsync();

            return existingTblKorpa;
        }

       public async Task IncreaseTotalPriceAsync(int proizUKorpiId, int korpaId,int proizvodId,int kolicina)
        {
            if (proizUKorpiId != null)
            {
                var proizvodUKorpiEntity = await makeUpDbContext.TblProizvodUkorpis.FirstOrDefaultAsync(p => p.ProizUkorpiId == proizUKorpiId);
                var proizvodEntity = await makeUpDbContext.TblProizvods.FirstOrDefaultAsync(p => p.ProizvodId == proizvodId);
                var ukupanIznos = kolicina * proizvodEntity.CenaPoKom;

                var korpaEntity = await makeUpDbContext.TblKorpas.FirstOrDefaultAsync(k => k.KorpaId == korpaId);
                if (korpaEntity == null)
                {
                    throw new Exception("The shoppingCart with the given ID was not found.");
                }
                
                    korpaEntity.UkupanIznos += ukupanIznos;
                    korpaEntity.BrProizvoda += kolicina;
                
                await makeUpDbContext.SaveChangesAsync();

            }
            else
            {
                throw new Exception("The shoppingCart with the given ID is empty.");
            }
           
        }
        public async Task ReduceTotalPriceAsync( int korpaId, int proizvodId,int kolicina)
        {
            var proizvodEntity = await makeUpDbContext.TblProizvods.FirstOrDefaultAsync(p => p.ProizvodId == proizvodId);
            var ukupanIznos = kolicina * proizvodEntity.CenaPoKom;
            var korpaEntity = await makeUpDbContext.TblKorpas.FirstOrDefaultAsync(k => k.KorpaId == korpaId);
            if (korpaEntity == null)
            {
                throw new Exception("The shoppingCart with the given ID was not found.");
            }
            korpaEntity.UkupanIznos -= ukupanIznos;
            korpaEntity.BrProizvoda -= kolicina;
            await makeUpDbContext.SaveChangesAsync();

        }

    }
}
