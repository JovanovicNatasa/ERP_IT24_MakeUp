using System;
using System.Collections.Generic;

namespace MakeupWebShop.Db;

public partial class TblBrend
{
    public int BrendId { get; set; }

    public string NazivBrenda { get; set; } = null!;

    public virtual ICollection<TblProizvod> TblProizvods { get; } = new List<TblProizvod>();
}
