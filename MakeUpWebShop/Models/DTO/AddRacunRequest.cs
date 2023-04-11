namespace MakeupWebShop.Models.DTO
{
    public class AddRacunRequest
    {
        public int RacunId { get; set; }
        public DateTime DatumKupovine { get; set; }

        public TimeSpan VremeKupovine { get; set; }

        public decimal IznosPost { get; set; }

        public decimal IznosSaPost { get; set; }

        public decimal? IznosPopusta { get; set; }

        public decimal? IznosSaPopustom { get; set; }

        public int KorpaId { get; set; }
    }
}
