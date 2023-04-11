using AutoMapper;

namespace MakeupWebShop.Profiles
{
    public class ProizvodProfile : Profile
    {
        public ProizvodProfile()
        {
            CreateMap<Db.TblProizvod, Models.DTO.Proizvod>()
                .ReverseMap();
        }
    }
}
