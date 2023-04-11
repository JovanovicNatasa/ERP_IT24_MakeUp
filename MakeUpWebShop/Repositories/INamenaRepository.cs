using MakeupWebShop.Db;

namespace MakeupWebShop.Repositories
{
    public interface INamenaRepository
    {
        public Task<IEnumerable<TblNamena>> GetAllAsync();
        Task<TblNamena> GetByIdAsync(int id);

        Task<TblNamena> AddAsync(TblNamena tblNamena);

        Task<TblNamena> UpdateAsync(int id, TblNamena tblNamena);
        Task<TblNamena> DeleteAsync(int id);
    }
}
