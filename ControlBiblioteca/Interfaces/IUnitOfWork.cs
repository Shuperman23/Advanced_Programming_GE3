namespace ControlBiblioteca.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Referencia a la interface de Autores
        /// </summary>
        IAutorRepository Autor { get; }

        /// <summary>
        /// Referencia a la interface de Generos LIterarios
        /// </summary>
        IGeneroLiterarioRepository GeneroLiterario { get; }

        /// <summary>
        /// Referencia a la interface de Libros
        /// </summary>
        ILibroRepository Libro { get; }

        /// <summary>
        /// Referencia a la interfaca de Parametro
        /// </summary>
        IParametroRepository Parametro { get; }

        /// <summary>
        /// Para guardar cambios en BD
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
