using System;
using System.Collections.Generic;

namespace MakeupWebShop.Db;

public partial class TblNamena
{
    public int NamenaId { get; set; }

    public string NazivNamene { get; set; } = null!;

    public virtual ICollection<TblProizvod> TblProizvods { get; } = new List<TblProizvod>();
}
