using MakeupWebShop.Db;

namespace MakeupWebShop.Repositories
{
    public interface IKorisnikRepository
    {
        public Task<IEnumerable<TblKorisnik>> GetAllAsync();
        Task<TblKorisnik> GetByIdAsync(int id);

        Task<TblKorisnik> GetByUsernameAsync(string username);
        Task<TblKorisnik> AddAsync(TblKorisnik tblKorisnik);

        Task<TblKorisnik> UpdateAsync(int id, TblKorisnik tblKorisnik);
        Task<TblKorisnik> UpdateUlogaAsync(int id, TblKorisnik tblKorisnik);
        Task<TblKorisnik> DeleteAsync(int id);
    }
}
