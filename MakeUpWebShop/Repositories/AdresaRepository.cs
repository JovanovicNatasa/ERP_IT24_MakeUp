using MakeupWebShop.Db;
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
    }
}
