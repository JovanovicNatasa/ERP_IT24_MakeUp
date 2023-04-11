using System;
using System.Collections.Generic;

namespace MakeupWebShop.Db;

public partial class TblKolekcija
{
    public int KolekcijaId { get; set; }

    public string NazivKolekcije { get; set; } = null!;

    public virtual ICollection<TblProizvod> TblProizvods { get; } = new List<TblProizvod>();
}
