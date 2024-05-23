using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Grupo
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public bool Grupo1 { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public int? CiudadesId { get; set; }

    public int? RepresentantesId { get; set; }

    public int? GenerosId { get; set; }

    public virtual ICollection<Albume> Albumes { get; set; } = new List<Albume>();

    public virtual ICollection<Artista> Artista { get; set; } = new List<Artista>();

    public virtual Ciudade? Ciudades { get; set; }

    public virtual Genero? Generos { get; set; }

    public virtual ICollection<Gira> Giras { get; set; } = new List<Gira>();

    public virtual Representante? Representantes { get; set; }
}
