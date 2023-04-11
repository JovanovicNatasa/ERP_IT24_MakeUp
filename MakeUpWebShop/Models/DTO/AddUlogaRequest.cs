using System.ComponentModel.DataAnnotations;

namespace MakeupWebShop.Models.DTO
{
    public class AddUlogaRequest
    {
        [MaxLength(30, ErrorMessage = "The Naziv Uloge cant be longer than 30 characters")]
        public string NazivUloge { get; set; } = null!;
        [MaxLength(90, ErrorMessage = "The Kratak Opis cant be longer than 90 characters")]
        public string KratakOpis { get; set; } = null!;
        [MaxLength(2, ErrorMessage = "The Sifra cant be longer than 2 characters")]
        public string Sifra { get; set; } = null!;
    }
}
