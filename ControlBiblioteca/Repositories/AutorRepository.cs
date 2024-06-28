using ControlBiblioteca.Data;
using ControlBiblioteca.DTOs;
using ControlBiblioteca.Interfaces;
using ControlBiblioteca.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ControlBiblioteca.Repositories
{
    public class AutorRepository : BaseRepository<Autor>, IAutorRepository
    {
        public AutorRepository(BIBLIOTECAContext context) : base(context)
        {

        }

        public async Task<List<Autor>> GetAutorAsync()
        {
            return await Context.Autors.ToListAsync();
        }

        public async Task<Autor?> GetAutorById(int autorId)
        {
            return await Context.Autors.FirstOrDefaultAsync(x => x.AutorId == autorId);
        }
        public async Task<StoredProcedureDto?> CreateNewAutorAsync(AutorDto resource)
        {
            var paramDescription = new SqlParameter("@Descripcion", resource.Descripcion);
            var paramEstado = new SqlParameter("@Estado", resource.Estado);

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spNewAutor] {paramDescription}, {paramEstado}").ToListAsync();

            return responseSp.FirstOrDefault();
        }
        public async Task<StoredProcedureDto?> UpdateAutorAsync(AutorDto resource)
        {
            var paramAutorId = new SqlParameter("@AutorId", resource.AutorId);
            var paramDescription = new SqlParameter("@Descripcion", resource.Descripcion);
            var paramEstado = new SqlParameter("@Estado", resource.Estado);

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spUpdateAutor] {paramAutorId}, {paramDescription}, {paramEstado}").ToListAsync();

            return responseSp.FirstOrDefault();
        }
    }
}
