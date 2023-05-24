
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using EasyCashIdentityProject.DataAccess.Concrete;
using EasyCashIdentityProject.Entity.Concrete;
using EasyCashIdentityProject.Presentation.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomRegisterValidator>();

//builder.Services.AddRazorPages().AddNToastNotifyNoty

builder.Services.AddNotyf(config =>
{
	config.DurationInSeconds = 5;
	config.IsDismissable = true;
	config.Position = NotyfPosition.TopRight;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseNotyf();
//app.MapRazorPages();
app.Run();
