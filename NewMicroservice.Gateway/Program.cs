using NewMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();

app.MapGet("/", () => "Yarp Gateway is running...");

app.Run();
