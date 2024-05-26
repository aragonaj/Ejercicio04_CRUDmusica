using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class Representante
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [StringLength(70, ErrorMessage = "Se excedió el número de caracteres permitido")]
    [Display(Name = "Nombre completo")]
    public string? NombreCompleto { get; set; }

    [Display(Name = "Fecha de nacimiento")]
    public DateOnly? FechaNacimiento { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Display(Name = "Identificación")]
    public string? Identificacion { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    [RegularExpression(".+\\@.+\\..+", ErrorMessage = "El e-mail introducido no es válido")]
    [Display(Name = "Correo electrónico")]
    public string? Mail { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [Display(Name = "Ciudad")]
    public int? CiudadesId { get; set; }

    public virtual Ciudade? Ciudades { get; set; }

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
}
