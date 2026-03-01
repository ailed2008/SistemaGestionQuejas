using Quejas.Entities;

public interface IQuejaService
{
    Task<Queja> RegistrarQueja(Queja nuevaQueja);

    Task<Queja?> ObtenerPorId(int id);

    Task<IEnumerable<Queja>> Consultar(int? idEstado, DateTime? inicio, DateTime? fin, int? idCategoria);
    Task CambiarEstatus(int id, int nuevoEstadoId, string usuario, string? comentario);
}