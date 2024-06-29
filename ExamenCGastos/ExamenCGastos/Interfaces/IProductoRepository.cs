using ExamenCGastos.DTOs;
using ExamenCGastos.Models;

namespace ExamenCGastos.Interfaces
{
    public interface IProductoRepository : IBaseRepository<Producto>
    {
        Task<List<Producto>> GetProductosAsync();
        Task<Producto?> GetProductoById(int productoId);
        Task<StoredProcedureDto?> CreateNewProducto(ProductoDto resource);
        Task<Producto> Update(Producto producto);
    }
}
