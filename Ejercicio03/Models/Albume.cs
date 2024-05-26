using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class Albume
{
    [Required(ErrorMessage ="Este campo es obligatorio")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? Nombre { get; set; }
    [Required(ErrorMessage ="Este campo es obligatorio")]
    public int? GenerosId { get; set; }
    [Required(ErrorMessage ="Este campo es obligatorio")]
    public int? GruposId { get; set; }
    public DateOnly? Fecha { get; set; }
    public virtual ICollection<Cancione> Canciones { get; set; } = new List<Cancione>();
    public virtual Genero? Generos { get; set; }
    [Required(ErrorMessage ="Este campo es obligatorio")]
    public virtual Grupo? Grupos { get; set; }
}
