﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class Paise
{
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    public string? Nombre { get; set; }

    public virtual ICollection<Ciudade> Ciudades { get; set; } = new List<Ciudade>();
}
