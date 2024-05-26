using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class Empleado
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int Id { get; set; }

    [Required(ErrorMessage ="Este campo es obligatorio")]
    [StringLength(70, ErrorMessage = "Se excedió el número de caracteres permitido")]
    [Display(Name = "Nombre completo")]
    public string? NombreCompleto { get; set; }

    [Required(ErrorMessage ="Este campo es obligatorio")]
    [StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    [Display(Name = "Rol")]
    public int? RolesId { get; set; }

    public virtual Role? Roles { get; set; }
}
