using MakeupWebShop.Db;

namespace MakeupWebShop.Repositories
{
    public interface IUlogaRepository
    {
        public Task<IEnumerable<TblUloga>> GetAllAsync();
        Task<TblUloga> GetByIdAsync(int id);

        Task<TblUloga> AddAsync(TblUloga tblUloga);

        Task<TblUloga> UpdateAsync(int id, TblUloga tblUloga);
        Task<TblUloga> DeleteAsync(int id);
    }
}
