using AutoMapper;
using MakeupWebShop.Db;
using MakeupWebShop.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace MakeupWebShop.Repositories
{
    public class ProizvodUKorpiRepository : IProizvodUKorpiRepository
    {
        private readonly MakeUpDbContext makeUpDbContext;


        public ProizvodUKorpiRepository(MakeUpDbContext makeUpDbContext)
        {
            this.makeUpDbContext = makeUpDbContext;
 
        }
        public async Task<TblProizvodUkorpi> AddAsync(TblProizvodUkorpi tblProizvodUkorpi)
        {
            await makeUpDbContext.AddAsync(tblProizvodUkorpi);
            await makeUpDbContext.SaveChangesAsync();

            return tblProizvodUkorpi;
        }

        public async Task<TblProizvodUkorpi> DeleteAsync(int id)
        {
            var tblProizvodUKorpi = await makeUpDbContext.TblProizvodUkorpis.FindAsync(id);
            if (tblProizvodUKorpi == null)
            {
                return null;
            }
            makeUpDbContext.TblProizvodUkorpis.Remove(tblProizvodUKorpi);
          
            await makeUpDbContext.SaveChangesAsync();
            return tblProizvodUKorpi;
        }

        public async Task<IEnumerable<TblProizvodUkorpi>> GetAllAsync()
        {
            return await makeUpDbContext.TblProizvodUkorpis.Include(x => x.Proizvod).Include(x => x.Korpa).ToListAsync();
        }

        public async Task<IEnumerable<TblProizvodUkorpi>> GetByKorpaIdAsync(int korpaId)
        {
            return await makeUpDbContext.TblProizvodUkorpis
                .Include(x => x.Proizvod)
                    .ThenInclude(p => p.Brend)
                .Include(x => x.Proizvod)
                    .ThenInclude(p => p.Namena)
                .Include(x => x.Proizvod)
                    .ThenInclude(p => p.Tip)
                .Include(x => x.Proizvod)
                    .ThenInclude(p => p.Kolekcija)
                .Where(x => x.KorpaId == korpaId)
                .ToListAsync();
        }


        public async Task<TblProizvodUkorpi> GetByIdAsync(int id)
        {
            return await makeUpDbContext.TblProizvodUkorpis.Include(x => x.Proizvod).Include(x => x.Korpa)
                .FirstOrDefaultAsync(x => x.ProizUkorpiId == id);
        }

        public async Task<TblProizvodUkorpi> UpdateAsync(int id, TblProizvodUkorpi tblProizvodUkorpi)
        {
           /* var oldTblProizvodUKorpi = await makeUpDbContext.TblProizvodUkorpis.FirstOrDefaultAsync(x => x.ProizUkorpiId == id);
            var existingTblProizvodUKorpi = oldTblProizvodUKorpi;*/

            var existingTblProizvodUKorpi = await makeUpDbContext.TblProizvodUkorpis.FirstOrDefaultAsync(x => x.ProizUkorpiId == id);
            if (existingTblProizvodUKorpi == null)
            {
                return null;
            }

            existingTblProizvodUKorpi.BrojKomada = tblProizvodUkorpi.BrojKomada;
            existingTblProizvodUKorpi.ProizvodId = existingTblProizvodUKorpi.ProizvodId;
            existingTblProizvodUKorpi.KorpaId = existingTblProizvodUKorpi.KorpaId;

            await makeUpDbContext.SaveChangesAsync();

            return existingTblProizvodUKorpi;
        }

    }
}
