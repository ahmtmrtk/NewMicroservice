using Asp.Versioning.Builder;
using NewMicroservice.Catalog.Api.Features.Categories.Create;
using NewMicroservice.Catalog.Api.Features.Categories.GetAll;
using NewMicroservice.Catalog.Api.Features.Categories.GetById;

namespace NewMicroservice.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories")
                .WithApiVersionSet(apiVersionSet).CreateCategoryGroupItemEndpoint().GetAllCategoryGroupItemEndpoint().GetByIdCategoryGroupItemEndpoint();
        }
    }
}
