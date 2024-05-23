using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
