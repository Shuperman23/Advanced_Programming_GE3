using ExamenCGastos.Data;
using ExamenCGastos.Interfaces;

namespace ExamenCGastos.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CGASTOSContext _context;
        private readonly Utilities _utilities;
        private readonly IConfiguration _configuration;

        private IParametroRepository _parametro = default!;
        private IAuthRepository _auth = default!;
        private IUsuarioRepository _usuario = default!;
        private IInventarioRepository _inventario = default!;
        private IProductoRepository _producto = default!;
        private IProveedorRepository _proveedor = default!;
       
        public IParametroRepository Parametro => _parametro ?? new ParametroRepository(_context);
        public IAuthRepository Auth => _auth ?? new AuthRepository(_configuration);
        public IUsuarioRepository Usuario => _usuario ?? new UsuarioRepository(_context, _utilities);
        public IInventarioRepository Inventario => _inventario ?? new InventarioRepository(_context);
        public IProductoRepository Producto => _producto ?? new ProductoRepository(_context);
        public IProveedorRepository Proveedor => _proveedor ?? new ProveedorRepository(_context);


        public UnitOfWork(CGASTOSContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
