using ControlBiblioteca.Data;
using ControlBiblioteca.DTOs;
using ControlBiblioteca.Interfaces;
using ControlBiblioteca.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ControlBiblioteca.Repositories
{
    public class LibroRepository : BaseRepository<Libro>, ILibroRepository
    {
        public LibroRepository(BIBLIOTECAContext context) : base(context) 
        {
        
        }

        public async Task<List<Libro>> GetLibroAsync()
        {
            return await Context.Libros.ToListAsync();
        }

        public async Task<Libro?> GetLibroById(int libroId)
        {
            return await Context.Libros.FirstOrDefaultAsync(x => x.LibroId == libroId);
        }

        public async Task<StoredProcedureDto?> CreateNewLibroAsync(LibroDto resource)
        {
            var paramgeneroLiterarioId = new SqlParameter("@GeneroLiterarioId", resource.GeneroLiterarioId);
            var paramautorId = new SqlParameter("@AutorId", resource.AutorId);
            var paramNombreLibro = new SqlParameter("@NombreLibro", resource.NombreLibro);
            var paramEstado = new SqlParameter("@Estado", resource.Estado);

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spNewLibro] {paramgeneroLiterarioId}, {paramautorId}, {paramNombreLibro},{paramEstado}").ToListAsync();

            return responseSp.FirstOrDefault();
        }

        public async Task<StoredProcedureDto?> UpdateLibroAsync(LibroDto resource)
        {
            var paramlibroId = new SqlParameter("@LibroId", resource.LibroId);
            var paramgeneroLiterarioId = new SqlParameter("@GeneroLiterarioId", resource.GeneroLiterarioId);
            var paramautorId = new SqlParameter("@AutorId", resource.AutorId);
            var paramNombreLibro = new SqlParameter("@NombreLibro", resource.NombreLibro);
            var paramEstado = new SqlParameter("@Estado", resource.Estado);

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spUpdateLibro] {paramlibroId}, {paramgeneroLiterarioId}, {paramautorId}, {paramNombreLibro},{paramEstado}").ToListAsync();

            return responseSp.FirstOrDefault();
        }
    }
}
