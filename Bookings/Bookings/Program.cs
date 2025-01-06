using Bookings.Application.Common.Interfaces;
using Bookings.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

//Creating an instance of teh web application builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//The db context is added as a service with sql server
builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//THis id teh dependency injection of teh villa repository the add scoped parameters define that whenever the 
//IVillarepository is being called it should know that teh implementation is non teh villa repository
builder.Services.AddScoped<IVillaRepository, VillaRepository>();

//Creating an instance of the web application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//THis enables teh use of static file like wwwroot folder
app.UseStaticFiles();

//This enables routing
app.UseRouting();

app.UseAuthorization();

//This enables routing for the controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//This command starts the server
app.Run();