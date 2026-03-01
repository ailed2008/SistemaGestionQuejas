using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quejas.Entities;

public partial class CatalogoCanales
{
    [Key]
    [Column("Id_canal")]
    public int IdCanal { get; set; }

    public string Nombre { get; set; } = null!;

    //public virtual ICollection<Queja> Quejas { get; set; } = new List<Queja>();
}
