using ExamenCGastos.DTOs;
using ExamenCGastos.Models;

namespace ExamenCGastos.Interfaces
{
    public interface IProductoRepository : IBaseRepository<Producto>
    {
        Task<List<Producto>> GetProductosAsync();
        Task<Producto?> GetProductoById(int productoId);
        Task<StoredProcedureDto?> CreateNewProductoAsync(ProductoDto resource);
        Task<StoredProcedureDto?> UpdateProductoAsync(ProductoDto resource);
    }
}
