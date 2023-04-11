using MakeupWebShop.Db;

namespace MakeupWebShop.Repositories
{
    public interface IProizvodUKorpiRepository
    {
        public Task<IEnumerable<TblProizvodUkorpi>> GetAllAsync();
        Task<TblProizvodUkorpi> GetByIdAsync(int id);

        Task<TblProizvodUkorpi> AddAsync(TblProizvodUkorpi tblProizvodUkorpi);

        Task<TblProizvodUkorpi> UpdateAsync(int id, TblProizvodUkorpi tblProizvodUkorpi);
        Task<TblProizvodUkorpi> DeleteAsync(int id);
    }
}
