using Microsoft.AspNetCore.Authentication.Cookies;
using NewMicroservice.Web.DeletgateHandlers;
using NewMicroservice.Web.Extensions;
using NewMicroservice.Web.Options;
using NewMicroservice.Web.Pages.Auth.SignUp;
using NewMicroservice.Web.Services;
using NewMicroservice.Web.Services.Refit;
using Refit;
using UdemyNewMicroservice.Web.Pages.Auth.SignIn;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddOptionsExt();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<SignUpService>();
builder.Services.AddHttpClient<SignInService>();
builder.Services.AddHttpClient<TokenService>();
builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped<AuthenticatedHttpClientHandler>();
builder.Services.AddScoped<ClientAuthenticatedHttpClientHandler>();

builder.Services.AddRefitClient<ICatalogRefitService>().ConfigureHttpClient(configure =>
{
    var microserviceOption = builder.Configuration.GetSection(nameof(MicroserviceOption)).Get<MicroserviceOption>();
    configure.BaseAddress = new Uri(microserviceOption!.CatalogMicroservice.BaseUrl);
}).AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
    .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();

builder.Services.AddAuthentication(configureOptions =>
{
    configureOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    configureOptions.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Auth/SignIn";
        options.ExpireTimeSpan = TimeSpan.FromDays(60);
        options.Cookie.Name = "NewMicroserviceAuthCookie";
        options.AccessDeniedPath = "/Auth/AccessDenided";
    });

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
