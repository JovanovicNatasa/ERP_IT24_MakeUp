using MakeupWebShop.Db;

namespace MakeupWebShop.Repositories
{
    public interface IRacunRepository
    {
        public Task<IEnumerable<TblRacun>> GetAllAsync();
        Task<TblRacun> GetByIdAsync(int id);

        Task<TblRacun> AddAsync(TblRacun tblRacun);

        Task<TblRacun> UpdateAsync(int id, TblRacun tblRacun);
        Task<TblRacun> DeleteAsync(int id);

        Task<TblRacun> GetByPaymentIntentIdAsync(string paymentIntentId);

        public int GetMaxRacunId();
        //Task<TblRacun> CalculateShipping(int racunId);
    }
}
