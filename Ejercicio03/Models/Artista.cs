using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Artista
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? GenerosId { get; set; }

    public DateOnly? FechaDeNacimiento { get; set; }

    public int? CiudadesId { get; set; }

    public int? GruposId { get; set; }

    public virtual Ciudade? Ciudades { get; set; }

    public virtual ICollection<FuncionesArtista> FuncionesArtista { get; set; } = new List<FuncionesArtista>();

    public virtual Genero? Generos { get; set; }

    public virtual Grupo? Grupos { get; set; }
}
