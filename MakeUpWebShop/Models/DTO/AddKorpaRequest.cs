namespace MakeupWebShop.Models.DTO
{
    public class AddKorpaRequest
    {
        public int KorpaId { get; set; }
        public int KorisnikId { get; set; }

        public int? UkupanIznos { get; set; }

        public int? BrProizvoda { get; set; }

        public bool? Popust { get; set; }

        public int? ProcenatPop { get; set; }
    }
}
