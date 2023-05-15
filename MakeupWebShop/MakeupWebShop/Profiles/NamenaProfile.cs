using AutoMapper;

namespace MakeupWebShop.Profiles
{
    public class NamenaProfile:Profile
    {
        public NamenaProfile()
        {
            CreateMap<Db.TblNamena, Models.DTO.Namena>()
                .ReverseMap();
        }
    }
}
