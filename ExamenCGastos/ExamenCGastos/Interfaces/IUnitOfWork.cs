namespace ExamenCGastos.Interfaces
{
    public interface IUnitOfWork
    {
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