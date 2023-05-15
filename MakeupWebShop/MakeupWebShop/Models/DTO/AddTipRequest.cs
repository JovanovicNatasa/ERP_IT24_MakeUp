using System.ComponentModel.DataAnnotations;

namespace MakeupWebShop.Models.DTO
{
    public class AddTipRequest
    {
        [MaxLength(50, ErrorMessage = "The Naziv Tipa cant be longer than 50 characters")]
        public string NazivTipa { get; set; } = null!;
    }
}
