using System;
using System.Collections.Generic;

namespace MakeupWebShop.Db;

public partial class TblTip
{
    public int TipId { get; set; }

    public string NazivTipa { get; set; } = null!;

    public virtual ICollection<TblProizvod> TblProizvods { get; } = new List<TblProizvod>();
}
