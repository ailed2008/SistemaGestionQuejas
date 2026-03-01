using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("HistorialQuejas")]
public class HistorialQueja
{
    [Key]
    [Column("Id_historial")]
    public int IdHistorial { get; set; }

    [Column("Id_queja")]
    public int IdQueja { get; set; }

    [Column("Id_estado_anterior")]
    public int IdEstadoAnterior { get; set; }

    [Column("Id_estado_nuevo")]
    public int IdEstadoNuevo { get; set; }

    [Column("Fecha_cambio")]
    public DateTime FechaCambio { get; set; }

    public string Usuario { get; set; } = null!;
    public string? Comentarios { get; set; }
}