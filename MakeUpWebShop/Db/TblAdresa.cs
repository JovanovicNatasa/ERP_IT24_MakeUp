using System;
using System.Collections.Generic;

namespace MakeupWebShop.Db;

public partial class TblAdresa
{
    public int AdresaId { get; set; }

    public string Grad { get; set; } = null!;

    public string Ulica { get; set; } = null!;

    public string Broj { get; set; } = null!;

    public int PostanskiBroj { get; set; }

    public virtual ICollection<TblKorisnik> TblKorisniks { get; } = new List<TblKorisnik>();
}
