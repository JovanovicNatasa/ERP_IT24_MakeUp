using AutoMapper;

namespace MakeupWebShop.Profiles
{
    public class UlogaProfile : Profile
    {
        public UlogaProfile()
        {
            CreateMap<Db.TblUloga, Models.DTO.Uloga>()
                .ReverseMap();
        }
    }
}
