using MakeupWebShop.Db;

namespace MakeupWebShop.Repositories
{
    public interface IAdresaRepository
    {
        public Task<IEnumerable<TblAdresa>> GetAllAsync();
        Task<TblAdresa> GetByIdAsync(int id);

        Task<TblAdresa> AddAsync(TblAdresa tblAdresa);

        Task<TblAdresa> UpdateAsync(int id, TblAdresa tblAdresa);
        Task<TblAdresa> DeleteAsync(int id);
    }
}
