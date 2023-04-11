using System;
using System.Collections.Generic;

namespace MakeupWebShop.Db;

public partial class TblUloga
{
    public int UlogaId { get; set; }

    public string NazivUloge { get; set; } = null!;

    public string KratakOpis { get; set; } = null!;

    public string Sifra { get; set; } = null!;

    public virtual ICollection<TblKorisnik> TblKorisniks { get; } = new List<TblKorisnik>();
}
