using System;
using System.Collections.Generic;

namespace MakeupWebShop.Db;

public partial class TblKorisnik
{
    public int KorisnikId { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string? Jmbg { get; set; }

    public string Email { get; set; } = null!;

    public string Kontakt { get; set; } = null!;

    public string? Username { get; set; }

    public string? Lozinka { get; set; }

    public int? BrojKupovina { get; set; }

    public int? AdresaId { get; set; }

    public int? UlogaId { get; set; }

    public virtual TblAdresa? Adresa { get; set; }

    public virtual ICollection<TblKorpa> TblKorpas { get; } = new List<TblKorpa>();

    public virtual TblUloga? Uloga { get; set; }
}
