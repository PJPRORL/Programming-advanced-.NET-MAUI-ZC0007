using AutoMapper;
using Examen_MVC.Models;
using Examen_MVC.ViewModels.Brewer;
using Examen_MVC.ViewModels.Cart;

namespace Examen_MVC.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Cart, GetItemInCartVM>()
                .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Coffee.Name))
                .ForMember(dest => dest.Image, x => x.MapFrom(src => src.Coffee.Image))
                .ForMember(dest => dest.TotalPrice, x => x.MapFrom(src => Math.Round(src.Coffee.Price * src.Quantity, 2)))
                .ForMember(dest => dest.Price, x => x.MapFrom(src => src.Coffee.Price));

            // Todo: Voeg extra mappings toe

            CreateMap<AddBrewerVM, Brewer>();
            CreateMap<EditBrewerDTO, Brewer>().ReverseMap();
        }
    }
}