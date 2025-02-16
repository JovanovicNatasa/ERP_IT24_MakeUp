﻿using MakeupWebShop.Db;
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
            var popust = korpa.ProcenatPop;
            tblRacun.IznosPost = 300;
            if (iznos >= 5000)
            {
                tblRacun.IznosPost = 0;
            }
            if (korpa.Popust == true)
            {
                tblRacun.IznosSaPost = (decimal)(iznos - iznos * popust / 100 + tblRacun.IznosPost);
            }
            else {
                tblRacun.IznosSaPost = (decimal)(iznos + tblRacun.IznosPost);
            }
            

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

        public async Task<TblRacun> GetByPaymentIntentIdAsync(string paymentIntentId)
        {
            return await makeUpDbContext.TblRacuns.Include(x => x.Korpa).FirstOrDefaultAsync(x => x.PaymentIntentId == paymentIntentId);
        }

        public int GetMaxRacunId()
        {
            var maxRacunId = makeUpDbContext.TblRacuns.Max(r => r.RacunId);
            return maxRacunId;
        }

    }
}
