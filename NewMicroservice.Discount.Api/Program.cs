using NewMicroservice.Discount.Api;
using NewMicroservice.Discount.Api.Features.Discounts;
using NewMicroservice.Discount.Api.Options;
using NewMicroservice.Discount.Api.Repositories;
using NewMicroservice.Shared.Extensions;
using UdemyNewMicroservice.Shared.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersioningExt();
builder.Services.AddOptionsExt();
builder.Services.AddRepositoryExt();
builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.AddCourseGroupEndpointExt(app.AddVersionSetExt());


app.Run();


