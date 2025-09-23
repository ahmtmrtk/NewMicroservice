
using Microsoft.Extensions.FileProviders;
using NewMicroservice.Bus;
using NewMicroservice.File.Api.Features.File;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddMassTransitExt(builder.Configuration);

builder.Services.AddVersioningExt();
builder.Services.AddCommonServiceExt(typeof(FileAssembly));

var app = builder.Build();
app.AddFileGroupEndpointExt(app.AddVersionSetExt());


app.UseStaticFiles();
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

