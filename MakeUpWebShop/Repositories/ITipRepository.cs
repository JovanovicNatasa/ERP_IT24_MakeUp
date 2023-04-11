using MakeupWebShop.Db;

namespace MakeupWebShop.Repositories
{
    public interface ITipRepository
    {
        public Task<IEnumerable<TblTip>> GetAllAsync();
        Task<TblTip> GetByIdAsync(int id);

        Task<TblTip> AddAsync(TblTip tblTip);

        Task<TblTip> UpdateAsync(int id, TblTip tblTip);
        Task<TblTip> DeleteAsync(int id);
    }
}
