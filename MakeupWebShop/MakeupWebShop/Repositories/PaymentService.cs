using MakeupWebShop.Db;
using Stripe;

namespace MakeupWebShop.Repositories
{
    public class PaymentService : IPaymentService
    {
        private readonly IRacunRepository racunRepository;
        private readonly IConfiguration configuration;
        private readonly MakeUpDbContext makeUpDbContext;

        public PaymentService(IRacunRepository racunRepository, IConfiguration configuration, MakeUpDbContext makeUpDbContext)
        {
            this.racunRepository = racunRepository;
            this.configuration = configuration;
            this.makeUpDbContext = makeUpDbContext;
        }

        public async Task<TblRacun> CreateUpdatePaymentIntent(int racunId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
            var racun = await racunRepository.GetByIdAsync(racunId);
            var sumPrice = racun.IznosSaPost;
            var service = new PaymentIntentService();
            PaymentIntent intent;

            if (string.IsNullOrEmpty(racun.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)sumPrice,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };
                intent = await service.CreateAsync(options);
                racun.PaymentIntentId = intent.Id;
                racun.ClientSecret = intent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)sumPrice
                };
                await service.UpdateAsync(racun.PaymentIntentId, options);
            }
            await racunRepository.UpdateAsync(racunId, racun);
            return racun;


        }

        public async Task<TblRacun> UpdateRacunPaymentFailed(string paymentIntentId)
        {
            var racun = await racunRepository.GetByPaymentIntentIdAsync(paymentIntentId);
            racun.Status = "Failed";
            await makeUpDbContext.SaveChangesAsync();
            return racun;
        }


        public async Task<TblRacun> UpdateRacunPaymentSucceeded(string paymentIntentId)
        {
            var racun = await racunRepository.GetByPaymentIntentIdAsync(paymentIntentId);
            racun.Status = "Succeeded";
            await makeUpDbContext.SaveChangesAsync();
            return racun;
        }
    }
}
