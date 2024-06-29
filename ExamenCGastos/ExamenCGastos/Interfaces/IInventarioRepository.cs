using ExamenCGastos.DTOs;
using ExamenCGastos.Models;

namespace ExamenCGastos.Interfaces
{
    public interface IInventarioRepository : IBaseRepository<Inventario>
    {
        Task<List<Inventario>> GetInventariosAsync();
        Task<Inventario?> GetInventarioById(int idMovimiento);
        Task<StoredProcedureDto?> CreateNewInventarioAsync(InventarioDto resource);
        Task<StoredProcedureDto?> UpdateInventarioAsync(InventarioDto resource);
    }
}