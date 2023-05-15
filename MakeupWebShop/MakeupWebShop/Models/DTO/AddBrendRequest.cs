using System.ComponentModel.DataAnnotations;

namespace MakeupWebShop.Models.DTO
{
    public class AddBrendRequest
    {
        [MaxLength(50, ErrorMessage = "The Naziv Brenda cant be longer than 50 characters")]
        public string NazivBrenda { get; set; } = null!;
    }
}
