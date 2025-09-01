using System.Text.Json.Serialization;

namespace NewMicroservice.Basket.Api.Dto
{
    public record BasketDto
    {
        [JsonIgnore] public Guid UserId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

        public BasketDto(Guid userId, List<BasketItemDto> items)
        {
            UserId = userId;
            Items = items;
        }
        public BasketDto()
        {

        }

    };


}
