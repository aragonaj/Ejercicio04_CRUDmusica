using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string? NombreCompleto { get; set; }

    public int? RolesId { get; set; }

    public virtual Role? Roles { get; set; }
}
