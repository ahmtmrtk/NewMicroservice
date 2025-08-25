using NewMicroservice.Catalog.Api.Features.Categories.Create;
using NewMicroservice.Catalog.Api.Features.Categories.GetAll;
using NewMicroservice.Catalog.Api.Features.Categories.GetById;

namespace NewMicroservice.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").CreateCategoryGroupItemEndpoint().GetAllCategoryGroupItemEndpoint().GetByIdCategoryGroupItemEndpoint();
        }
    }
}
