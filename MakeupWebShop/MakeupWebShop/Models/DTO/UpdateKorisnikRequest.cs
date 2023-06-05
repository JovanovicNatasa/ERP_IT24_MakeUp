using System.ComponentModel.DataAnnotations;

namespace MakeupWebShop.Models.DTO
{
    public class UpdateKorisnikRequest
    {
        public string Ime { get; set; } = null!;
        [MaxLength(50, ErrorMessage = "The Prezime cant be longer than 50 characters")]
        public string Prezime { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "The Email cant be longer than 50 characters")]
        public string Email { get; set; } = null!;
        [MaxLength(20, ErrorMessage = "The Kontakt cant be longer than 220 characters")]
        public string Kontakt { get; set; } = null!;
        [MaxLength(50, ErrorMessage = "The Username cant be longer than 50 characters")]
        public string? Username { get; set; }

        public string? Lozinka { get; set; }

        public int? BrojKupovina { get; set; }

        public int? AdresaId { get; set; }

        //public int? UlogaId { get; set; }
    }
}
