using System.ComponentModel.DataAnnotations;

namespace MakeupWebShop.Models.DTO
{
    public class UpdateNamenaRequest
    {
        [MaxLength(50, ErrorMessage = "The Naziv Namene cant be longer than 50 characters")]
        public string NazivNamene { get; set; } = null!;
    }
}
