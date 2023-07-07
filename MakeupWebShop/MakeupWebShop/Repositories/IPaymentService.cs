using MakeupWebShop.Db;

namespace MakeupWebShop.Repositories
{
    public interface IPaymentService
    {
        Task<TblRacun> CreateUpdatePaymentIntent(int racunId);
        Task<TblRacun> UpdateRacunPaymentSucceeded(string paymentIntentId);
        Task<TblRacun> UpdateRacunPaymentFailed(string paymentIntentId);
    }
}
