using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class Concierto
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int Id { get; set; }

    [Required(ErrorMessage ="Este campo es obligatorio")]
    [Display(Name = "Gira")]
    public int? GirasId { get; set; }

    [Required(ErrorMessage ="Este campo es obligatorio")]
    public DateOnly? Fecha { get; set; }

    [Required(ErrorMessage ="Este campo es obligatorio")]
    [Display(Name = "Ciudad")]
    public int? CiudadesId { get; set; }

    [Display(Name = "Dirección")]
    [StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    public string? Direccion { get; set; }

    public virtual Ciudade? Ciudades { get; set; }
    public virtual Gira? Giras { get; set; }
}
