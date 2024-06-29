using ExamenCGastos.DTOs;
using ExamenCGastos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamenCGastos.Interfaces
{
    public interface IProveedorRepository : IBaseRepository<Proveedor>
    {
        Task<List<Proveedor>> GetProveedoresAsync();
        Task<Proveedor?> GetProveedorById(int Id);
        Task<StoredProcedureDto?> CreateNewProveedorAsync(ProveedorDto resource);
        Task<StoredProcedureDto?> UpdateProveedorAsync(ProveedorDto resource);
    }
}
