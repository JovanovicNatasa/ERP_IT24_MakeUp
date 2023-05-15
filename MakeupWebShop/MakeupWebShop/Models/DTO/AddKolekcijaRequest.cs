using System.ComponentModel.DataAnnotations;

namespace MakeupWebShop.Models.DTO
{
    public class AddKolekcijaRequest
    {
        [MaxLength(50, ErrorMessage = "The Naziv Kolekcije cant be longer than 50 characters")]
        public string NazivKolekcije { get; set; } = null!;
    }
}
