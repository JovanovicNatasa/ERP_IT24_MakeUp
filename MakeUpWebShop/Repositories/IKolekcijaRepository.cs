using MakeupWebShop.Db;

namespace MakeupWebShop.Repositories
{
    public interface IKolekcijaRepository
    {
        public Task<IEnumerable<TblKolekcija>> GetAllAsync();
        Task<TblKolekcija> GetByIdAsync(int id);

        Task<TblKolekcija> AddAsync(TblKolekcija tblKolekcija);

        Task<TblKolekcija> UpdateAsync(int id, TblKolekcija tblKolekcija);
        Task<TblKolekcija> DeleteAsync(int id);
    }
}
