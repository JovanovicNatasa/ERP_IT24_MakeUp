using System;
using System.Collections.Generic;

namespace MakeupWebShop.Db;

public partial class TblKorpa
{
    public int KorpaId { get; set; }

    public int KorisnikId { get; set; }

    public decimal? UkupanIznos { get; set; }

    public int? BrProizvoda { get; set; }

    public bool? Popust { get; set; }

    public int? ProcenatPop { get; set; }

    public virtual TblKorisnik Korisnik { get; set; } = null!;

    public virtual ICollection<TblProizvodUkorpi> TblProizvodUkorpis { get; } = new List<TblProizvodUkorpi>();

    public virtual ICollection<TblRacun> TblRacuns { get; } = new List<TblRacun>();
}
