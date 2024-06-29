using ExamenCGastos.DTOs;
using ExamenCGastos.Models;

namespace ExamenCGastos.Interfaces
{
    public interface IInventarioRepository : IBaseRepository<Inventario>
    {
        Task<List<Inventario>> GetInventariosAsync();
        Task<Inventario?> GetInventarioById(int idMovimiento);
        Task<StoredProcedureDto?> CreateNewInventario(InventarioDto resource);
        Task<Inventario> Update(Inventario inventario);
    }
}