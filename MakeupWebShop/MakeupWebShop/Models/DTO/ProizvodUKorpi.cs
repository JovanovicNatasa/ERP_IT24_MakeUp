using MakeupWebShop.Db;

namespace MakeupWebShop.Models.DTO
{
    public class ProizvodUKorpi
    {
        public int ProizUkorpiId { get; set; }

        public int BrojKomada { get; set; }

        public int ProizvodId { get; set; }

        public int KorpaId { get; set; }


        public virtual TblKorpa Korpa { get; set; } = null!;

        public virtual TblProizvod Proizvod { get; set; } = null!;
    }
}
