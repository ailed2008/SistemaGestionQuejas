using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Quejas")] 
public class Queja
{
    [Key]
    [Column("Id_queja")] 
    public int IdQueja { get; set; }

    public string Folio { get; set; } = null!;
    public string Titulo { get; set; } = null!;
    public string Descripcion { get; set; } = null!;

    [Column("Nombre_cliente")] 
    public string NombreCliente { get; set; } = null!;

    public string Correo { get; set; } = null!;

    [Column("Id_categoria")]
    public int IdCategoria { get; set; }

    [Column("Id_canal")]
    public int IdCanal { get; set; }

    [Column("Id_estado")]
    public int IdEstado { get; set; }

    [Column("Fecha_registro")]
    public DateTime? FechaRegistro { get; set; }

    [Column("Fecha_actualizacion")]
    public DateTime? FechaActualizacion { get; set; }
}