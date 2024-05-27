using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class Gira
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Display(Name = "Grupo")]
    public int? GruposId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Display(Name = "Fecha de inicio")]
    public DateOnly? FechaInicio { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Display(Name = "Fecha de finalización")]
    public DateOnly? FechaFin { get; set; }

    public virtual ICollection<Concierto> Conciertos { get; set; } = new List<Concierto>();

    public virtual Grupo? Grupos { get; set; }
}
