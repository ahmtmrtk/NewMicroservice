using Microsoft.EntityFrameworkCore;
using NewMicroservice.Bus;
using NewMicroservice.Order.Application.BackgroundServices;
using NewMicroservice.Shared.Extensions;
using NewMicroservice.Shared.Options;
using NewMicroService.Order.Api;
using NewMicroService.Order.Api.Endpoint.Orders;
using NewMicroService.Order.Application;
using NewMicroService.Order.Application.Contracts.Refit;
using NewMicroService.Order.Application.Contracts.Refit.Payment;
using NewMicroService.Order.Application.Contracts.Repositories;
using NewMicroService.Order.Application.Contracts.UnitOfWork;
using NewMicroService.Order.Persistance;
using NewMicroService.Order.Persistance.Repositories;
using NewMicroService.Order.Persistance.UnitOfWork;
using Refit;
using UdemyNewMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

builder.Services.AddRefitConfigurationExt(builder.Configuration);
builder.Services.AddHostedService<CheckPaymentStatusOrderBackgroundService>();

builder.Services.AddVersioningExt();
builder.Services.AddCommonMassTransitExt(builder.Configuration);

var app = builder.Build();

app.AddOrderGroupEndpointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();


app.Run();

