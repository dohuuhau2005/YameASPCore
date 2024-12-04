using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using yame.Data;
using yame.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("yameContextConnection") ?? throw new InvalidOperationException("Connection string 'yameContextConnection' not found.");
//Add connection into database by using context
IServiceCollection serviceCollection = builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<Users, IdentityRole>(Option =>
{
    Option.Password.RequireNonAlphanumeric = false;
    Option.Password.RequiredLength = 6;
    Option.Password.RequireLowercase = false;
    Option.Password.RequireUppercase = false;
    Option.User.RequireUniqueEmail = false;
    Option.SignIn.RequireConfirmedAccount = false;
    Option.SignIn.RequireConfirmedPhoneNumber = false;
    Option.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Đảm bảo bạn có route cho CartController
    endpoints.MapControllerRoute(
        name: "cart",
        pattern: "Cart/{action=Index}/{id?}");
});//add app map razor for login and register page
app.MapRazorPages();

app.Run();
