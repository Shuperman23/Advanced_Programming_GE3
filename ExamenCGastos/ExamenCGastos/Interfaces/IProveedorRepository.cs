using ExamenCGastos.DTOs;
using ExamenCGastos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamenCGastos.Interfaces
{
    public interface IProveedorRepository : IBaseRepository<Proveedor>
    {
        Task<List<Proveedor>> GetProveedoresAsync();
        Task<Proveedor?> GetProveedorById(int idProveedor);
        Task<StoredProcedureDto?> CreateNewProveedor(ProveedorDto resource);
        Task<Proveedor> Update(Proveedor proveedor);
    }
}
