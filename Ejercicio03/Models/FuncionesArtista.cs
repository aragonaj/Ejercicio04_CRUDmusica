using System;
using System.Collections.Generic;

namespace Ejercicio03.Models;

public partial class FuncionesArtista
{
    public int Id { get; set; }

    public int? FuncionesId { get; set; }

    public int? ArtistasId { get; set; }

    public virtual Artista? Artistas { get; set; }

    public virtual Funcione? Funciones { get; set; }
}
