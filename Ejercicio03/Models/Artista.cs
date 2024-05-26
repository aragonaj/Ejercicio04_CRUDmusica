using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class Artista
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int Id { get; set; }

    [StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? Nombre { get; set; }

    [Display(Name = "Género")]
    public int? GenerosId { get; set; }

    [Display(Name = "Fecha de nacimiento")]
    public DateOnly? FechaDeNacimiento { get; set; }

    [Display(Name = "Ciudad")]
    public int? CiudadesId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Display(Name = "Grupo")]
    public int? GruposId { get; set; }

    public virtual Ciudade? Ciudades { get; set; }

    public virtual ICollection<FuncionesArtista> FuncionesArtista { get; set; } = new List<FuncionesArtista>();

    public virtual Genero? Generos { get; set; }

    public virtual Grupo? Grupos { get; set; }
}
