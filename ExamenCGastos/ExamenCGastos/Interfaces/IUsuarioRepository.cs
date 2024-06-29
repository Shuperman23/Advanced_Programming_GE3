using ExamenCGastos.DTOs;
using ExamenCGastos.Models;

namespace ExamenCGastos.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<List<Usuario>> GetUsuarioAsync();
        Task<Usuario?> GetUsuarioById(int IdUsuario);
        Task<StoredProcedureDto?> CreateNewUsuarioAsync(UsuarioDTO resource);
        Task<StoredProcedureDto?> UpdateUsuarioAsync(UsuarioDTO resource);
    }
}