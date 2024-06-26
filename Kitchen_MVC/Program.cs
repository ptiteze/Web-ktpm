using Kitchen_MVC.Data;
using Kitchen_MVC.DependencyInjection.Extensions;
using Kitchen_MVC.Helper;
using Kitchen_MVC.Singleton;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddConfigureApplication();
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

//c?u h�nh logger factory method
Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
	.CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
	// Configure session options here
	options.Cookie.Name = "YourSessionCookieName";
	options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
	// Add other session options as needed
});

// Add other services
builder.Services.AddMvc();

builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
