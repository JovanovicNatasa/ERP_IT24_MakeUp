using System.ComponentModel.DataAnnotations;

namespace MakeupWebShop.Models.DTO
{
    public class UpdateKolekcijaRequest
    {

        [MaxLength(50, ErrorMessage = "The Naziv Kolekcije cant be longer than 50 characters")]
        public string NazivKolekcije { get; set; } = null!;
    }
}
