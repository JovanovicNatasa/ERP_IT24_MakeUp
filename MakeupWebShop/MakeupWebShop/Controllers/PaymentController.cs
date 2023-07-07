using MakeupWebShop.Db;
using MakeupWebShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace MakeupWebShop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService paymentService;
        private readonly ILogger<PaymentController> logger;
        private const string WhSecret = "whsec_f0ea03698e139c7d4ec7bf3f5b1ec170ebdac8748c48beaaf58e806f262059a9";

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            this.paymentService = paymentService;
            this.logger = logger;
        }

        [Authorize]
        [HttpPost("{racunId}")]
        public async Task<ActionResult<TblRacun>> CreateUpdatePaymentIntent(int racunId)
        {
            return await paymentService.CreateUpdatePaymentIntent(racunId);
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json= await new StreamReader(Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);

            PaymentIntent intent;
            TblRacun racun;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent=(PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("Payment succeeded ", intent.Id);
                    //Update racun with new status
                    racun = await paymentService.UpdateRacunPaymentSucceeded(intent.Id);
                    logger.LogInformation("Racun status updated to succeeded ", racun.RacunId);
                    break;

                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("Payment failed ", intent.Id);
                    //Update racun with new status
                    racun = await paymentService.UpdateRacunPaymentFailed(intent.Id);
                    logger.LogInformation("Racun status updated to failed ", racun.RacunId);
                    break;
            }
            return new EmptyResult();
        }
    }
}
