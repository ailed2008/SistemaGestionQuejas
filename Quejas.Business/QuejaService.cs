using Quejas.Data.Context;
using Quejas.Entities;
using Microsoft.EntityFrameworkCore;

namespace Quejas.Business
{
    public class QuejaService : IQuejaService
    {
        private readonly AppDbContext _context;

        public QuejaService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Queja> RegistrarQueja(Queja nuevaQueja)
        {
            //Regla: Toda queja debe iniciar en "Registrada" (Estatus 1)
            nuevaQueja.IdEstado = 1;
            nuevaQueja.FechaRegistro = DateTime.Now;
            // Generar folio 
            nuevaQueja.Folio = $"F-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

            _context.Quejas.Add(nuevaQueja);
            await _context.SaveChangesAsync();
            return nuevaQueja;
        }

        public async Task CambiarEstatus(int idQueja, int nuevoEstadoId, string usuario, string comentario)
        {

            var queja = await _context.Quejas.FindAsync(idQueja);
            if (queja == null) throw new Exception("Queja no encontrada");

            // Regla: No puede modificarse si está "Cerrada" (Id 6)
            if (queja.IdEstado == 6) throw new Exception("La queja ya está cerrada.");

            // Regla: No puede cerrarse si no está "Resuelta" (Id 4)
            if (nuevoEstadoId == 6 && queja.IdEstado != 4)
                throw new Exception("Debe estar en estatus Resuelta para poder cerrar.");

            // Registrar en el historial
            var historial = new HistorialQueja
            {
                IdQueja = idQueja,
                IdEstadoAnterior = queja.IdEstado,
                IdEstadoNuevo = nuevoEstadoId,
                FechaCambio = DateTime.Now,
                Usuario = usuario,
                Comentarios = comentario
            };

            queja.IdEstado = nuevoEstadoId;
            queja.FechaActualizacion = DateTime.Now;
            _context.HistorialQuejas.Add(historial);
            _context.Entry(queja).Property(q => q.FechaRegistro).IsModified = false;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Queja>> Consultar(int? idEstado, DateTime? desde, DateTime? hasta, int? idCategoria)
        {
            var query = _context.Quejas.AsQueryable();
            if (idEstado.HasValue) query = query.Where(q => q.IdEstado == idEstado);
            if (desde.HasValue) query = query.Where(q => q.FechaRegistro >= desde);
            if (hasta.HasValue) query = query.Where(q => q.FechaRegistro <= hasta);

            return await query.ToListAsync();
        }

        public async Task<Queja?> ObtenerPorId(int id)
        {
            return await _context.Quejas.FindAsync(id);
        }

    }
}
