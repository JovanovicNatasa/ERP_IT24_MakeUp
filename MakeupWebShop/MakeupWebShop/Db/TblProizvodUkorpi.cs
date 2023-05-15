using System;
using System.Collections.Generic;

namespace MakeupWebShop.Db;

public partial class TblProizvodUkorpi
{
    public int ProizUkorpiId { get; set; }

    public int BrojKomada { get; set; }

    public int ProizvodId { get; set; }

    public int KorpaId { get; set; }

    public virtual TblKorpa Korpa { get; set; } = null!;

    public virtual TblProizvod Proizvod { get; set; } = null!;
}
