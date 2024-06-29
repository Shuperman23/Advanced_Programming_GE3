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

        public async Task<StoredProcedureDto?> CreateNewProveedor(ProveedorDto resource)
        {
            var paramNombre = new SqlParameter("@Nombre", resource.Nombre);
            var paramDireccion = new SqlParameter("@Direccion", resource.Direccion);
            var paramTelefono = new SqlParameter("@TelefonoContacto", resource.TelefonoContacto);
            var paramEmail = new SqlParameter("@EmailContacto", resource.EmailContacto);

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spNewProveedor] {paramNombre}, {paramDireccion}, {paramTelefono}, {paramEmail}").ToListAsync();

            return responseSp.FirstOrDefault();
        }

        public async Task<Proveedor> Update(Proveedor proveedor)
        {
            Context.Proveedores.Update(proveedor);
            await Context.SaveChangesAsync();
            return proveedor;
        }
    }
}
