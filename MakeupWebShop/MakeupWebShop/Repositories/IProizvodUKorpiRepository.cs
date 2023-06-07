using MakeupWebShop.Db;
using System.Security.Principal;
using MakeupWebShop.Models.DTO;

namespace MakeupWebShop.Repositories
{
    public interface IProizvodUKorpiRepository
    {
        public Task<IEnumerable<TblProizvodUkorpi>> GetAllAsync();
        Task<TblProizvodUkorpi> GetByIdAsync(int id);
        Task<IEnumerable<TblProizvodUkorpi>> GetByKorpaIdAsync(int korpaId);

        Task<TblProizvodUkorpi> AddAsync(TblProizvodUkorpi tblProizvodUkorpi);

        Task<TblProizvodUkorpi> UpdateAsync(int id, TblProizvodUkorpi tblProizvodUkorpi);
        Task<TblProizvodUkorpi> DeleteAsync(int id);
       
    }
}
