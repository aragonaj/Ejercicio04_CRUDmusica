// WEBGRAFÍA
// https://www.youtube.com/watch?v=WplklpczJX8 
// https://www.youtube.com/watch?v=U0zYxZ6OzDM -JQuery

//--no-pluralize
//--use-Database-name
//--force

//[ModelMetadataType(Typeof(NombreModelo))]
//public partial class NombreModelo
//se copia el documento NombreModelo

// La tabla álbumes da fallo al no haberla actualizado
// La creación de una nueva función de un miembro de la banda ("Create new - Funciones artista") da error

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
