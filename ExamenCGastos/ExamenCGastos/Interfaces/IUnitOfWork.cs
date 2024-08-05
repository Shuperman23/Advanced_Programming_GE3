namespace ExamenCGastos.Interfaces
{
    public interface IUnitOfWork
    {

        IUsuarioRepository Usuario { get; }

        IParametroRepository Parametro { get; }

        IAuthRepository Auth { get; }

        IInventarioRepository Inventario { get; }

        IProductoRepository Producto { get; }

        IProveedorRepository Proveedor { get; }

        Task<int> SaveChangesAsync();
    }
}