namespace ControlBiblioteca.Interfaces
{
    /// <summary>
    /// Interfaz base para agrupar todos los métodos comunes de los repositorios.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidad.</typeparam>
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Obtener todos los registros de entidades.
        /// </summary>
        /// <returns>IQueryable que representa todas las entidades de tipo TEntity.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Obtener una entidad por su ID.
        /// </summary>
        /// <param name="id">ID de la entidad.</param>
        /// <returns>Una tarea que representa la operación asíncrona. La tarea contiene la entidad encontrada o null si no se encuentra.</returns>
        Task<TEntity?> FindByIdAsync(int id);

        /// <summary>
        /// Crear un nuevo registro.
        /// </summary>
        /// <param name="entity">Entidad a crear.</param>
        void Create(TEntity entity);

        /// <summary>
        /// Actualizar una entidad.
        /// </summary>
        /// <param name="entity">Entidad a actualizar.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Eliminar una entidad.
        /// </summary>
        /// <param name="entity">Entidad a eliminar.</param>
        void Delete(TEntity entity);
    }
}