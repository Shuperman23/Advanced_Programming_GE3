using ExamenCGastos.Data;
using ExamenCGastos.DTOs;
using ExamenCGastos.Interfaces;
using ExamenCGastos.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ExamenCGastos.Repositories
{
    public class ProveedorRepository : BaseRepository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(CGASTOSContext context) : base(context)
        {
        }

        public async Task<List<Proveedor>> GetProveedoresAsync()
        {
            return await Context.Proveedores.ToListAsync();
        }

        public async Task<Proveedor?> GetProveedorById(int idProveedor)
        {
            return await Context.Proveedores.FirstOrDefaultAsync(x => x.Id == idProveedor);
        }

        public async Task<StoredProcedureDto?> CreateNewProveedorAsync(ProveedorDto resource)
        {
            var paramNombre = new SqlParameter("@Nombre", resource.Nombre);
            var paramTelefono = new SqlParameter("@TelefonoContacto", resource.TelefonoContacto);
            var paramEmail = new SqlParameter("@EmailContacto", resource.EmailContacto);
            var paramDireccion = new SqlParameter("@Direccion", resource.Direccion);

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spNewProveedor] {paramNombre}, {paramTelefono}, {paramEmail}, {paramDireccion}").ToListAsync();

            return responseSp.FirstOrDefault();
        }

        public async Task<StoredProcedureDto?> UpdateProveedorAsync(ProveedorDto resource)
        {
            var paramIdProveedor = new SqlParameter("@Id", resource.Id);
            var paramNombre = new SqlParameter("@Nombre", resource.Nombre);
            var paramTelefono = new SqlParameter("@TelefonoContacto", resource.TelefonoContacto);
            var paramEmail = new SqlParameter("@EmailContacto", resource.EmailContacto);
            var paramDireccion = new SqlParameter("@Direccion", resource.Direccion);

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spUpdateProveedor] {paramIdProveedor}, {paramNombre}, {paramTelefono}, {paramEmail}, {paramDireccion}").ToListAsync();

            return responseSp.FirstOrDefault();
        }
    }
}
