using MongoDB.Driver;
using NewMicroservice.Discount.Api.Options;


namespace NewMicroservice.Discount.Api.Repositories
{
    public static class RepositoryExt
    {
        public static IServiceCollection AddRepositoryExt(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<MongoOption>();
                return new MongoClient(options.ConnectionString);
            });
            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<MongoClient>();
                var options = sp.GetRequiredService<MongoOption>();
                return AppDbContext.Create(client.GetDatabase(options.DatabaseName));
            });

            return services;

        }
    }
}
