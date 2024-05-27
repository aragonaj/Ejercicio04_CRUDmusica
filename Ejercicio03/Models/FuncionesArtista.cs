using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio03.Models;

public partial class FuncionesArtista
{
    public int Id { get; set; }

    public int? FuncionesId { get; set; }

    //[Required(ErrorMessage = "Este campo es obligatorio")]
    //[StringLength(50, ErrorMessage = "Se excedió el número de caracteres permitido")]
    //[Display(Name = "Artista")]
    public int? ArtistasId { get; set; }

    public virtual Artista? Artistas { get; set; }

    public virtual Funcione? Funciones { get; set; }
}
