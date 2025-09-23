using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection; // Ensure this is present
using Microsoft.OpenApi.Models; // Ensure this is present
using NewMicroservice.Basket.Api.Features.Baskets;
using NewMicroservice.Bus;
using NewMicroservice.Shared.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI; // Add this using directive
using UdemyNewMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(BasketAssembly));
builder.Services.AddVersioningExt();
builder.Services.AddMassTransitExt(builder.Configuration);
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddScoped<BasketService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

var app = builder.Build();
app.AddBasketGroupEndpointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // This now works because of the added using directive
    app.MapOpenApi();
}
app.UseAuthentication();
app.UseAuthorization();


app.Run();
