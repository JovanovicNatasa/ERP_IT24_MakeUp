using AutoMapper;

namespace MakeupWebShop.Profiles
{
    public class TipProfile : Profile
    {
        public TipProfile()
        {
            CreateMap<Db.TblTip, Models.DTO.Tip>()
                .ReverseMap();
        }
    }
}
