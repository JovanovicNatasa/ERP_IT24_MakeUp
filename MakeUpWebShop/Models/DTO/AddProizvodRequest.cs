using System.ComponentModel.DataAnnotations;

namespace MakeupWebShop.Models.DTO
{
    public class AddProizvodRequest
    {
        [MaxLength(50, ErrorMessage = "The Model cant be longer than 50 characters")]
        public string? Model { get; set; }
        [MaxLength(250, ErrorMessage = "The Sastav cant be longer than 250 characters")]
        public string Sastav { get; set; } = null!;
        [MaxLength(250, ErrorMessage = "The Nacin Upotrebe cant be longer than 250 characters")]
        public string NacinUpotrebe { get; set; } = null!;

        public decimal CenaPoKom { get; set; }

        public int KolicinaNaStanju { get; set; }

        public int BrendId { get; set; }

        public int NamenaId { get; set; }

        public int TipId { get; set; }

        public int KolekcijaId { get; set; }
    }
}
