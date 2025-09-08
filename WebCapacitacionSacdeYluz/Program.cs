using Microsoft.EntityFrameworkCore;
using WebCapacitacionSacdeYluz.BL.Services;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data;
using WebCapacitacionSacdeYluz.Data.Repositories;
using WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICalzadoRepository, CalzadoRepository>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();

builder.Services.AddScoped<ICalzadoService, CalzadoService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();




builder.Services.AddDbContext<WebCapacitacionSacdeLuzDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebCapacitacionSacdeLuzDbContext")));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
