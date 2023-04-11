using MakeupWebShop.Db;
namespace MakeupWebShop.Models.DTO
{
    public class Adresa
    {
        public int AdresaId { get; set; }

        public string Grad { get; set; } = null!;

        public string Ulica { get; set; } = null!;

        public string Broj { get; set; } = null!;

        public int PostanskiBroj { get; set; }

    }
}
