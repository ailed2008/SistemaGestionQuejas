using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quejas.Entities;

public partial class CatalogoEstado
{
    [Key]
    [Column("Id_estado")]
    public int IdEstado { get; set; }

    public string Nombre { get; set; } = null!;

    //public virtual ICollection<HistorialQueja> HistorialQuejaIdEstadoAnteriorNavigations { get; set; } = new List<HistorialQueja>();

    public virtual ICollection<HistorialQueja> HistorialQuejaIdEstadoNuevoNavigations { get; set; } = new List<HistorialQueja>();

    public virtual ICollection<Queja> Quejas { get; set; } = new List<Queja>();
}
