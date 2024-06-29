using ExamenCGastos.Data;
using ExamenCGastos.DTOs;
using ExamenCGastos.Interfaces;
using ExamenCGastos.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ExamenCGastos.Repositories
{
    public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(CGASTOSContext context) : base(context)
        {
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            return await Context.Productos.ToListAsync();
        }

        public async Task<Producto?> GetProductoById(int productoId)
        {
            return await Context.Productos.FirstOrDefaultAsync(x => x.Id == productoId);
        }

        public async Task<StoredProcedureDto?> CreateNewProducto(ProductoDto resource)
        {
            var paramNombre = new SqlParameter("@Nombre", resource.Nombre);
            var paramProveedor = new SqlParameter("@Proveedor", resource.ProveedorId);

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spNewProducto] {paramNombre}, {paramProveedor}").ToListAsync();

            return responseSp.FirstOrDefault();
        }

        public async Task<Producto> Update(Producto producto)
        {
            Context.Productos.Update(producto);
            await Context.SaveChangesAsync();
            return producto;
        }
    }
}
