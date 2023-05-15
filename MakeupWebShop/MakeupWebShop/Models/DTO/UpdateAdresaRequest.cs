using System.ComponentModel.DataAnnotations;

namespace MakeupWebShop.Models.DTO
{
    public class UpdateAdresaRequest
    {
        [MaxLength(20, ErrorMessage = "The Grad cant be longer than 20 characters")]
        public string Grad { get; set; } = null!;
        [MaxLength(50, ErrorMessage = "The Ulica cant be longer than 50 characters")]
        public string Ulica { get; set; } = null!;
        [MaxLength(13, ErrorMessage = "The Broj cant be longer than 13 characters")]
        public string Broj { get; set; } = null!;

        public int PostanskiBroj { get; set; }
    }
}
