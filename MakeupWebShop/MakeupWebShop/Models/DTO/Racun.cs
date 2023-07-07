using MakeupWebShop.Db;

namespace MakeupWebShop.Models.DTO
{
    public class Racun
    {
        public int RacunId { get; set; }

        public DateTime DatumKupovine { get; set; }

        public TimeSpan VremeKupovine { get; set; }

        public decimal IznosPost { get; set; }

        public decimal IznosSaPost { get; set; }

        public decimal? IznosPopusta { get; set; }

        public decimal IznosSaPopustom { get; set; }

        public int KorpaId { get; set; }
        public virtual TblKorpa Korpa { get; set; } = null!;

        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }

        public string? Status { get; set; }
    }
}
