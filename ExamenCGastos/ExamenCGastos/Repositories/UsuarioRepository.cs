using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ExamenCGastos.Data;
using ExamenCGastos.DTOs;
using ExamenCGastos.Interfaces;
using ExamenCGastos.Models;

namespace ExamenCGastos.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly Utilities _utilities;

        public UsuarioRepository(CGASTOSContext context, Utilities utilities) : base(context)
        {
            _utilities = utilities;
        }

        public async Task<List<Usuario>> GetUsuarioAsync()
        {
            return await Context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioById(int usuarioId)
        {
            return await Context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);
        }

        public async Task<StoredProcedureDto?> CreateNewUsuarioAsync(UsuarioDTO resource)
        {
            var paramNombre = new SqlParameter("@Nombre", resource.Nombre);
            var paramCorreo = new SqlParameter("@Correo", resource.Correo);
            var paramClave = new SqlParameter("@Clave", _utilities.EncriptarContrasena(resource.Clave));

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spNewUsuario] {paramNombre}, {paramCorreo}, {paramClave}").ToListAsync();

            return responseSp.FirstOrDefault();
        }

        public async Task<StoredProcedureDto?> UpdateUsuarioAsync(UsuarioDTO resource)
        {
            var paramUsuarioId = new SqlParameter("@IdUsuario", resource.IdUsuario);
            var paramNombre = new SqlParameter("@Nombre", resource.Nombre);
            var paramCorreo = new SqlParameter("@Correo", resource.Correo);
            var paramClave = new SqlParameter("@Clave", _utilities.EncriptarContrasena(resource.Clave));

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spUpdateUsuario] {paramUsuarioId}, {paramNombre}, {paramCorreo}, {paramClave}").ToListAsync();

            return responseSp.FirstOrDefault();
        }
    }
}