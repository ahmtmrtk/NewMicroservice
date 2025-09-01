using NewMicroservice.Basket.Api.Dto;

namespace NewMicroservice.Basket.Api.Features.Baskets
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<Data.Basket, BasketDto>().ReverseMap();
            CreateMap<Data.BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}