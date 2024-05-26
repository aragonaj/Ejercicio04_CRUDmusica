using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class Grupo
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int Id { get; set; }

    //[Required(ErrorMessage = "Este campo es obligatorio")]
    [StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Display(Name = "Miembro del grupo")]
    public bool Grupo1 { get; set; }

    [Display(Name = "Fecha de creación")]
    public DateOnly? FechaCreacion { get; set; }

    [Display(Name = "Ciudad")]
    public int? CiudadesId { get; set; }

    [Display(Name = "Representante")]
    public int? RepresentantesId { get; set; }

    [Display(Name = "Género")]
    public int? GenerosId { get; set; }

    public virtual ICollection<Albume> Albumes { get; set; } = new List<Albume>();

    public virtual ICollection<Artista> Artista { get; set; } = new List<Artista>();

    public virtual Ciudade? Ciudades { get; set; }

    public virtual Genero? Generos { get; set; }

    public virtual ICollection<Gira> Giras { get; set; } = new List<Gira>();

    public virtual Representante? Representantes { get; set; }
}