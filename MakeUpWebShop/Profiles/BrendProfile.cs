using AutoMapper;

namespace MakeupWebShop.Profiles
{
    public class BrendProfile : Profile
    {
        public BrendProfile()
        {
            CreateMap<Db.TblBrend, Models.DTO.Brend>()
                .ReverseMap();
        }
    }
}
