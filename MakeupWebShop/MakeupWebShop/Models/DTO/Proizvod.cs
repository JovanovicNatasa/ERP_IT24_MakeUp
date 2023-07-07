
using MakeupWebShop.Db;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupWebShop.Models.DTO
{
    public class Proizvod
    {
        public int ProizvodId { get; set; }

        public string? Model { get; set; }

        public string Sastav { get; set; } = null!;

        public string NacinUpotrebe { get; set; } = null!;

        public decimal CenaPoKom { get; set; }

        public int KolicinaNaStanju { get; set; }

        public int BrendId { get; set; }
        public int NamenaId { get; set; }
        public int TipId { get; set; }
        public int KolekcijaId { get; set; }

        public virtual TblBrend Brend { get; set; } = null!;


        public virtual TblKolekcija Kolekcija { get; set; } = null!;

        public virtual TblNamena Namena { get; set; } = null!;

        public virtual ICollection<TblProizvodUkorpi> TblProizvodUkorpis { get; } = new List<TblProizvodUkorpi>();

        public virtual TblTip Tip { get; set; } = null!;

    }
}
