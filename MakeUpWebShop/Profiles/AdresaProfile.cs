using AutoMapper;

namespace MakeupWebShop.Profiles
{
    public class AdresaProfile : Profile
    {
        public AdresaProfile()
        {
            CreateMap<Db.TblAdresa, Models.DTO.Adresa>()
                .ReverseMap();
        }
    }
}
