using ControlBiblioteca.Data;
using ControlBiblioteca.Interfaces;
using ControlBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlBiblioteca.Repositories
{
    /// <summary>
    /// Implementación del repositorio para la entidad Parametro.
    /// </summary>
    public class ParametroRepository : BaseRepository<Parametro>, IParametroRepository
    {
        // Constructor que recibe el contexto de datos como parámetro y lo pasa a la clase base.
        public ParametroRepository(BIBLIOTECAContext context) : base(context)
        {
        }

        /// <summary>
        /// Obtiene un parámetro por su nombre de forma asíncrona.
        /// </summary>
        /// <param name="nombre">Nombre del parámetro a buscar.</param>
        /// <returns>Una tarea que representa la operación asíncrona. La tarea contiene el parámetro encontrado o null si no se encuentra.</returns>
        public async Task<Parametro?> GetParametroByNombreAsync(string nombre)
        {
            // Utiliza FirstOrDefaultAsync para buscar un parámetro por su nombre de forma asíncrona.
            // Si no se encuentra ningún parámetro con el nombre proporcionado, devuelve null.
            return await Context.Parametros.FirstOrDefaultAsync(x => x.Nombre.Equals(nombre));
        }
    }
}