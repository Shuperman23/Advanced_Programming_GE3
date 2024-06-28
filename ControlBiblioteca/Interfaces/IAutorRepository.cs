using ControlBiblioteca.Models;
using ControlBiblioteca.DTOs;

namespace ControlBiblioteca.Interfaces
{
    public interface IAutorRepository : IBaseRepository<Autor>
    {
        Task<List<Autor>> GetAutorAsync();
        Task<Autor?> GetAutorById(int autorId);
        Task<StoredProcedureDto?> CreateNewAutorAsync(AutorDto resource);
        Task<StoredProcedureDto?> UpdateAutorAsync(AutorDto resource);
    }
}
