using MakeupWebShop.Db;
using MakeupWebShop.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MakeupWebShop.Repositories
{
    public class AdresaRepository : IAdresaRepository
    {
        private readonly MakeUpDbContext makeUpDbContext;

        public AdresaRepository(MakeUpDbContext makeUpDbContext)
        {
            this.makeUpDbContext = makeUpDbContext;
        }

        public async Task<TblAdresa> AddAsync(TblAdresa tblAdresa)
        {
           /* SqlConnection connection = new SqlConnection("DESKTOP-7G4I5HD\\SQLEXPRESS");
            using (SqlCommand command = new SqlCommand("SELECT NEXT VALUE FOR AdresaID_seq", connection))
            {
                //Execute the SqlCommand and get the next sequence value
                int nextSequenceValue = (int)command.ExecuteScalar();

                //Use the nextSequenceValue to create your object with a new ID
                tblAdresa.AdresaId = nextSequenceValue;
            }*/
            await makeUpDbContext.AddAsync(tblAdresa);
            await makeUpDbContext.SaveChangesAsync();
            return tblAdresa;
        }

        public async Task<TblAdresa> DeleteAsync(int id)
        {
            var tblAdresa = await makeUpDbContext.TblAdresas.FindAsync(id);
            if (tblAdresa == null)
            {
                return null;
            }
            makeUpDbContext.TblAdresas.Remove(tblAdresa);
            await makeUpDbContext.SaveChangesAsync();
            return tblAdresa;
        }

        public async Task<IEnumerable<TblAdresa>> GetAllAsync()
        {
            return await makeUpDbContext.TblAdresas.ToListAsync();
        }

        public async Task<TblAdresa> GetByIdAsync(int id)
        {
            return await makeUpDbContext.TblAdresas.FirstOrDefaultAsync(x => x.AdresaId == id);
        }

        public async Task<TblAdresa> UpdateAsync(int id, TblAdresa tblAdresa)
        {
            var existingTblAdresa = await makeUpDbContext.TblAdresas.FirstOrDefaultAsync(x => x.AdresaId == id);
            if (existingTblAdresa == null)
            {
                return null;
            }
            existingTblAdresa.Grad= tblAdresa.Grad;
            existingTblAdresa.Ulica = tblAdresa.Ulica;
            existingTblAdresa.Broj = tblAdresa.Broj;
            existingTblAdresa.PostanskiBroj = tblAdresa.PostanskiBroj;

            await makeUpDbContext.SaveChangesAsync();

            return existingTblAdresa;
        }
        public async Task<TblAdresa> GetAdresaByDetailsAsync(AddKorisnikRequest addKorisnikRequest)
        {
            var adresa = await makeUpDbContext.TblAdresas
        .FirstOrDefaultAsync(a => a.Grad == addKorisnikRequest.Adresa.Grad &&
                               a.Ulica == addKorisnikRequest.Adresa.Ulica &&
                               a.Broj == addKorisnikRequest.Adresa.Broj &&
                               a.PostanskiBroj == addKorisnikRequest.Adresa.PostanskiBroj);

            if (adresa == null)
            {
                adresa = new TblAdresa
                {
                    Grad = addKorisnikRequest.Adresa.Grad,
                    Ulica = addKorisnikRequest.Adresa.Ulica,
                    Broj = addKorisnikRequest.Adresa.Broj,
                    PostanskiBroj = addKorisnikRequest.Adresa.PostanskiBroj
                };

                makeUpDbContext.TblAdresas.Add(adresa);
                await makeUpDbContext.SaveChangesAsync();
            }

            return adresa;
        }

        // Method to get address by details for UpdateKorisnikRequest
        public async Task<TblAdresa> GetAdresaByDetailsForUpdateAsync(UpdateKorisnikRequest updateKorisnikRequest)
        {
            var adresa = await makeUpDbContext.TblAdresas
                .FirstOrDefaultAsync(a => a.Grad == updateKorisnikRequest.Adresa.Grad &&
                                           a.Ulica == updateKorisnikRequest.Adresa.Ulica &&
                                           a.Broj == updateKorisnikRequest.Adresa.Broj &&
                                           a.PostanskiBroj == updateKorisnikRequest.Adresa.PostanskiBroj);

            if (adresa == null)
            {
                adresa = new TblAdresa
                {
                    Grad = updateKorisnikRequest.Adresa.Grad,
                    Ulica = updateKorisnikRequest.Adresa.Ulica,
                    Broj = updateKorisnikRequest.Adresa.Broj,
                    PostanskiBroj = updateKorisnikRequest.Adresa.PostanskiBroj
                };

                makeUpDbContext.TblAdresas.Add(adresa);
                await makeUpDbContext.SaveChangesAsync();
            }

            return adresa;
        }
    }



}
