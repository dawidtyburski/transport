using AutoMapper;
using transport.Models;

namespace transport
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile() 
        {
            CreateMap<Order, OrderDto>()
                .ForMember(o => o.PickupCity, p => p.MapFrom(r => r.PickupAdress.City))
                .ForMember(o => o.PickupPostCode, p => p.MapFrom(r => r.PickupAdress.PostCode))
                .ForMember(o => o.PickupCountry, p => p.MapFrom(r => r.PickupAdress.Country))
                .ForMember(o => o.DestCity, p => p.MapFrom(r => r.DestinationAdress.City))
                .ForMember(o => o.DestPostCode, p => p.MapFrom(r => r.DestinationAdress.PostCode))
                .ForMember(o => o.DestCountry, p => p.MapFrom(r => r.DestinationAdress.Country))
                .ForMember(o => o.FullName, p => p.MapFrom(r => r.CustomUser.FullName))
                .ForMember(o => o.Email, p => p.MapFrom(r => r.CustomUser.UserName))
                .ForMember(o => o.PhoneNumber, p => p.MapFrom(r => r.CustomUser.PhoneNumber));


            CreateMap<CreateOrderModel, Order>()
                .ForMember(o => o.PickupAdress, p => p.MapFrom(dto => new PickupAdress { City = dto.PickupCity, PostCode = dto.PickupPostCode, Country = dto.PickupCountry }))
                .ForMember(o => o.DestinationAdress, p => p.MapFrom(dto => new DestinationAdress { City = dto.DestCity, PostCode = dto.DestPostCode, Country = dto.DestCountry }));
        }
    }
}
