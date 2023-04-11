using MakeupWebShop.Db;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeupWebShop.Repositories
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly MakeUpDbContext makeUpDbContext;

        public KorisnikRepository(MakeUpDbContext makeUpDbContext)
        {
            this.makeUpDbContext = makeUpDbContext;
        }
        public async Task<TblKorisnik> AddAsync(TblKorisnik tblKorisnik)
        {
         
                await makeUpDbContext.AddAsync(tblKorisnik);
                await makeUpDbContext.SaveChangesAsync();
                return tblKorisnik;
            
        }

        public async Task<TblKorisnik> DeleteAsync(int id)
        {
            var tblKorisnik = await makeUpDbContext.TblKorisniks.FindAsync(id);
            if (tblKorisnik == null)
            {
                return null;
            }
            makeUpDbContext.TblKorisniks.Remove(tblKorisnik);
            await makeUpDbContext.SaveChangesAsync();
            return tblKorisnik;
        }

        public async Task<IEnumerable<TblKorisnik>> GetAllAsync()
        {
            return await makeUpDbContext.TblKorisniks.Include(x => x.Adresa).Include(x => x.Uloga)
                .ToListAsync();
        }

        public async Task<TblKorisnik> GetByIdAsync(int id)
        {
            return await makeUpDbContext.TblKorisniks.Include(x => x.Adresa).Include(x => x.Uloga)
                .FirstOrDefaultAsync(x => x.KorisnikId == id);
        }

        public async Task<TblKorisnik> GetByUsernameAsync(string username)
        {
            return await makeUpDbContext.TblKorisniks.Include(x => x.Adresa).Include(x => x.Uloga)
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<TblKorisnik> UpdateAsync(int id, TblKorisnik tblKorisnik)
        {
            var existingTblKorisnik = await makeUpDbContext.TblKorisniks.FirstOrDefaultAsync(x => x.KorisnikId == id);
            if (existingTblKorisnik == null)
            {
                return null;
            }

            existingTblKorisnik.Ime = tblKorisnik.Ime;
            existingTblKorisnik.Prezime = tblKorisnik.Prezime;
            existingTblKorisnik.Jmbg = tblKorisnik.Jmbg;
            existingTblKorisnik.Email = tblKorisnik.Email;
            existingTblKorisnik.Kontakt = tblKorisnik.Kontakt;
            existingTblKorisnik.Username = tblKorisnik.Username;
            existingTblKorisnik.Lozinka = tblKorisnik.Lozinka;
            existingTblKorisnik.BrojKupovina = tblKorisnik.BrojKupovina;
            existingTblKorisnik.AdresaId = tblKorisnik.AdresaId;
            existingTblKorisnik.UlogaId = tblKorisnik.UlogaId;
            await makeUpDbContext.SaveChangesAsync();

            return existingTblKorisnik;
        }

        /*public async Task<TblKorisnik> UpdateUlogaAsync(int id, TblKorisnik tblKorisnik)
        {
            var existingTblKorisnik = await makeUpDbContext.TblKorisniks.FirstOrDefaultAsync(x => x.KorisnikId == id);
            if (existingTblKorisnik == null)
            {
                return null;
            }
            existingTblKorisnik.UlogaId = tblKorisnik.UlogaId;

            await makeUpDbContext.SaveChangesAsync();

            return existingTblKorisnik;
        }*/
    }
}
