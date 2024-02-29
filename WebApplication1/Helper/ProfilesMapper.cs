using AutoMapper;
using Store99.Dto.Brand;
using Store99.Dto.Sho;
using Store99.Dto.Shoe;
using Store99.Dto.ShoeFile;
using Store99.Models;

namespace Store99.Helper
{
    public class ProfilesMapper: Profile
    {
        // mapea los profiles de cada entidad/modelo
        public ProfilesMapper()
        {
            // mapeamos las entitdades con el dto
            CreateMap<Shoe, ShoeDto>();
                // explicitamos la relación de muchos files a un shoe
                // .ForMember(dest => dest.ShoeFiles, opt => opt.MapFrom(src => src.ShoeFile));
            CreateMap<ShoeDto, Shoe>();
            CreateMap<CreateShoeDto, Shoe>();
            CreateMap<Shoe, CreateShoeDto>();
            CreateMap<Brand, BrandDto>();

            //CreateMap<ShoeFile, ShoeFileDto>();
        }
    }
}
