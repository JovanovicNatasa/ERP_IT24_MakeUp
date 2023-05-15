using AutoMapper;

namespace MakeupWebShop.Profiles
{
    public class RacunProfile : Profile
    {
        public RacunProfile()
        {
            CreateMap<Db.TblRacun, Models.DTO.Racun>()
                .ReverseMap();
        }
    }
}
