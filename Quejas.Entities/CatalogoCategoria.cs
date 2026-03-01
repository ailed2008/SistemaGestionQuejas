using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quejas.Entities;

public partial class CatalogoCategoria
{
    [Key]
    [Column("Id_categoria")]
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    //public virtual ICollection<Queja> Quejas { get; set; } = new List<Queja>();
}
