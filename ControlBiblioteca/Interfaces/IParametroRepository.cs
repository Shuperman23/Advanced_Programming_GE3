using ControlBiblioteca.Models;
using System.Reflection.Metadata;

namespace ControlBiblioteca.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de Parametro que hereda de IBaseRepository.
    /// </summary>
    public interface IParametroRepository : IBaseRepository<Parametro>
    {
        /// <summary>
        /// Obtiene un parámetro por su nombre de forma asíncrona.
        /// </summary>
        /// <param name="nombre">Nombre del parámetro a buscar.</param>
        /// <returns>Una tarea que representa la operación asíncrona. La tarea contiene el parámetro encontrado o null si no se encuentra.</returns>
        Task<Parametro?> GetParametroByNombreAsync(string nombre);
    }
}