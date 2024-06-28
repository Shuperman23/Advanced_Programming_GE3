using ControlBiblioteca.Data;
using ControlBiblioteca.DTOs;
using ControlBiblioteca.Interfaces;
using ControlBiblioteca.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ControlBiblioteca.Repositories
{
    public class GeneroLiterarioRepository : BaseRepository<GeneroLiterario>, IGeneroLiterarioRepository
    {
        public GeneroLiterarioRepository(BIBLIOTECAContext context) : base(context) 
        { 
        
        }
        public async Task<List<GeneroLiterario>> GetGeneroLiterarioAsync()
        {
            return await Context.GeneroLiterarios.ToListAsync();
        }

        public async Task<GeneroLiterario?> GetGeneroLiterarioById(int generoLiterarioId) //Con el signo ? hacemos referencia a posible envío de null
        {
            return await Context.GeneroLiterarios.FirstOrDefaultAsync(x => x.GeneroLiterarioID == generoLiterarioId);
        }
        public async Task<StoredProcedureDto?> CreateNewGeneroLiterarioAsync(GeneroLiterarioDto resource)
        {
            var paramDescription = new SqlParameter("@Descripcion", resource.Descripcion);
            var paramEstado = new SqlParameter("@Estado", resource.Estado);

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spNewGeneroLiterario] {paramDescription}, {paramEstado}").ToListAsync();

            return responseSp.FirstOrDefault();
        }

        public async Task<StoredProcedureDto?> UpdateGeneroLiterarioAsync(GeneroLiterarioDto resource)
        {
            var paramGeneroLiterarioId = new SqlParameter("@GeneroLiterarioId", resource.GeneroLiterarioID);
            var paramDescription = new SqlParameter("@Descripcion", resource.Descripcion);
            var paramEstado = new SqlParameter("@Estado", resource.Estado);

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spUpdateGeneroLiterario] {paramGeneroLiterarioId}, {paramDescription}, {paramEstado}").ToListAsync();

            return responseSp.FirstOrDefault();
        }

    }
}
