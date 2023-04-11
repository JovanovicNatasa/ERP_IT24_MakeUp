using MakeupWebShop.Db;

namespace MakeupWebShop.Models.DTO
{
    public class Korpa
    {
        public int KorpaId { get; set; }

        public int KorisnikId { get; set; }

        public decimal? UkupanIznos { get; set; }

        public int? BrProizvoda { get; set; }

        public bool? Popust { get; set; }

        public int? ProcenatPop { get; set; }
        public virtual TblKorisnik Korisnik { get; set; } = null!;
    }
}
