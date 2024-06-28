using ControlBiblioteca.Data;
using ControlBiblioteca.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlBiblioteca.Repositories
{
    /// <summary>
    /// Implementación del repositorio base.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de repositorio/entidad.</typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        // Contexto de datos protegido que se utiliza para interactuar con la base de datos.
        protected BIBLIOTECAContext Context { get; set; }

        // Constructor del repositorio base que recibe el contexto de datos como parámetro.
        protected BaseRepository(BIBLIOTECAContext context)
        {
            // Asigna el contexto de datos al campo protegido.
            Context = context;
        }

        // Método para obtener todas las entidades del tipo TEntity de la base de datos.
        public IQueryable<TEntity> GetAll() => Context.Set<TEntity>().AsNoTracking();

        // Método asíncrono para encontrar una entidad por su ID.
        // Devuelve la entidad si se encuentra, de lo contrario devuelve null.
        public async Task<TEntity?> FindByIdAsync(int id) => await Context.Set<TEntity>().FindAsync(id);

        // Método para crear una nueva entidad en la base de datos.
        public void Create(TEntity entity) => Context.Set<TEntity>().Add(entity);

        // Método para actualizar una entidad existente en la base de datos.
        public void Update(TEntity entity) => Context.Set<TEntity>().Update(entity);

        // Método para eliminar una entidad de la base de datos.
        public void Delete(TEntity entity) => Context.Set<TEntity>().Remove(entity);
    }
}