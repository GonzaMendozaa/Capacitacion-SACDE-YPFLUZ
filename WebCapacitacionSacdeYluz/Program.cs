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
builder.Services.AddScoped<ITiendaRepository, TiendaRepository>();
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
<<<<<<<<< Temporary merge branch 1
builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
builder.Services.AddScoped<ICompraRepository, CompraRepository>();
>>>>>>>>> Temporary merge branch 2

builder.Services.AddScoped<ICalzadoService, CalzadoService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<ICompraService, CompraService>();
builder.Services.AddScoped<IMarcaService, MarcaService>();
builder.Services.AddScoped<ICompraService, CompraService>();
builder.Services.AddScoped<ICompraService, CompraService>();
>>>>>>>>> Temporary merge branch 2




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
    name: "calzados",
    pattern: "Calzados/{action=Index}/{id?}",
    defaults: new { controller = "Calzados" });

app.MapControllerRoute(
    name: "tienda",
    pattern: "Tienda/{action=Index}/{id?}",
    defaults: new { controller = "Tienda" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
