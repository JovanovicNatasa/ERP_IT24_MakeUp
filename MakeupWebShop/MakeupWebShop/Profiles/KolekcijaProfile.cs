using AutoMapper;

namespace MakeupWebShop.Profiles
{
    public class KolekcijaProfile:Profile
    {
        public KolekcijaProfile()
        {
            CreateMap<Db.TblKolekcija, Models.DTO.Kolekcija>()
                .ReverseMap();
        }
    }
}
