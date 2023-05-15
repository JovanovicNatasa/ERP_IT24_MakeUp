using MakeupWebShop.Db;

namespace MakeupWebShop.Repositories
{
    public interface IBrendRepository
    {
        public Task<IEnumerable<TblBrend>> GetAllAsync();
        Task<TblBrend> GetByIdAsync(int id);

        Task<TblBrend> AddAsync(TblBrend tblBrend);

        Task<TblBrend> UpdateAsync(int id, TblBrend tblBrend);
        Task<TblBrend> DeleteAsync(int id);
    }
}
