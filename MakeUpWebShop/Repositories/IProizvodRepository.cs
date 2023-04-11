using MakeupWebShop.Db;

namespace MakeupWebShop.Repositories
{
    public interface IProizvodRepository
    {
        public Task<IEnumerable<TblProizvod>> GetAllAsync();
        Task<TblProizvod> GetByIdAsync(int id);

        Task<TblProizvod> AddAsync(TblProizvod tblProizvod);

        Task<TblProizvod> UpdateAsync(int id, TblProizvod tblProizvod);
        Task<TblProizvod> DeleteAsync(int id);
    }
}
