namespace MakeupWebShop.Models.DTO
{
    public class UpdateKorpaRequest
    {
        public int KorisnikId { get; set; }

        public decimal? UkupanIznos { get; set; }

        public int? BrProizvoda { get; set; }

        public bool? Popust { get; set; }

        public int? ProcenatPop { get; set; }
    }
}
