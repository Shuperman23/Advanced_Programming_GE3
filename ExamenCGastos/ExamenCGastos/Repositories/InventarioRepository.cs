using Elfie.Serialization;
using ExamenCGastos.Data;
using ExamenCGastos.DTOs;
using ExamenCGastos.Interfaces;
using ExamenCGastos.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ExamenCGastos.Repositories
{
    public class InventarioRepository : BaseRepository<Inventario>, IInventarioRepository
    {
        public InventarioRepository(CGASTOSContext context) : base(context)
        {
        }

        public async Task<List<Inventario>> GetInventariosAsync()
        {
            return await Context.Inventarios.ToListAsync();
        }

        public async Task<Inventario?> GetInventarioById(int idMovimiento)
        {
            return await Context.Inventarios.FirstOrDefaultAsync(x => x.Id == idMovimiento);
        }

        public async Task<StoredProcedureDto?> CreateNewInventarioAsync(InventarioDto resource)
        {
            
            var paramIdProducto = new SqlParameter("@IdProducto", resource.ProductoId);
            var paramTipoMovimiento = new SqlParameter("@TipoMovimiento", resource.TipoMovimiento); 
            var paramCantidad = new SqlParameter("@Cantidad", resource.Cantidad);
            var paramPrecio = new SqlParameter("@Precio", resource.Precio);
            var paramFechaMovimiento = new SqlParameter("@FechaMovimiento", resource.FechaMovimiento);
            var paramFechaCaducidad = new SqlParameter("@FechaCaducidad", (object)resource.FechaCaducidad ?? DBNull.Value);

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spNewInventario] {paramIdProducto},{paramTipoMovimiento}, {paramCantidad}, {paramPrecio}, {paramFechaMovimiento}, {paramFechaCaducidad}").ToListAsync();

            return responseSp.FirstOrDefault();
        }

        public async Task<StoredProcedureDto?> UpdateInventarioAsync(InventarioDto resource)
        {
            var paramInventarioId = new SqlParameter("@Id", resource.Id);
            var paramIdProducto = new SqlParameter("@IdProducto", resource.ProductoId);
            var paramTipoMovimiento = new SqlParameter("@TipoMovimiento", resource.TipoMovimiento);
            var paramCantidad = new SqlParameter("@Cantidad", resource.Cantidad);
            var paramPrecio = new SqlParameter("@Precio", resource.Precio);
            var paramFechaMovimiento = new SqlParameter("@FechaMovimiento", resource.FechaMovimiento);
            var paramFechaCaducidad = new SqlParameter("@FechaCaducidad", (object)resource.FechaCaducidad ?? DBNull.Value);

            var responseSp = await Context.Set<StoredProcedureDto>().FromSql($"EXECUTE [dbo].[spNewInventario] {paramInventarioId},{paramIdProducto},{paramTipoMovimiento}, {paramCantidad}, {paramPrecio}, {paramFechaMovimiento}, {paramFechaCaducidad}").ToListAsync();

            return responseSp.FirstOrDefault();
        }
    }
}
