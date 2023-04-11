using AutoMapper;

namespace MakeupWebShop.Profiles
{
    public class ProizvodUKorpiProfile : Profile
    {
        public ProizvodUKorpiProfile()
        {
            CreateMap<Db.TblProizvodUkorpi, Models.DTO.ProizvodUKorpi>()
                .ReverseMap();
        }
    }
}
