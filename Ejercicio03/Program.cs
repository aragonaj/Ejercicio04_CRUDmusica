//--no-pluralize
//--use-Database-name
//--force

using Microsoft.EntityFrameworkCore.Storage;

using Microsoft.EntityFrameworkCore;
using Ejercicio03.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<GrupoBContext>( options => options.UseSqlServer("server=musicagrupos.database.windows.net;database=GrupoB;Integrated Security=True \""));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
