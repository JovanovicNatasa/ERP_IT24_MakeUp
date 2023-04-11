using MakeupWebShop.Db;

namespace MakeupWebShop.Repositories
{
    public interface IKorpaRepository
    {
        public Task<IEnumerable<TblKorpa>> GetAllAsync();
        Task<TblKorpa> GetByIdAsync(int id);

        Task<TblKorpa> AddAsync(TblKorpa tblKorpa);

        Task<TblKorpa> UpdateAsync(int id, TblKorpa tblKorpa);
        Task<TblKorpa> DeleteAsync(int id);
    }
}
