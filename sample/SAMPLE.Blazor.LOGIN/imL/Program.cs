using Microsoft.AspNetCore.Authentication.Cookies;

using SAMPLE.Blazor.LOGIN;
using SAMPLE.Blazor.LOGIN.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

//################################################################
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".SAMPLE.Blazor.LOGIN";
    options.IdleTimeout = TimeSpan.FromSeconds(8);
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.EventsType = typeof(CustomCookieAuthenticationEvents);
    });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<CustomCookieAuthenticationEvents>();
//################################################################

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

//################################################################
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
//################################################################

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
