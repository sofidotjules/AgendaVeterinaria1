using AgendaVeterinaria1.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("DefaultDatabase");
services.AddControllersWithViews();
//services.AddDbContext<AgendaDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultDatabase")));
//builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<AgendaDBContext>(options => options.UseSqlServer(connectionString));
//services
//    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(config =>
//    {
//        config.LoginPath = "/Home/Login";
//        config.AccessDeniedPath = "/Home/NoAutorizado";
//        config.LogoutPath = "/Home/Login";
//        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
//    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




