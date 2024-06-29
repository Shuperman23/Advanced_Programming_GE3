using ExamenCGastos.Data;
using ExamenCGastos.Interfaces;

namespace ExamenCGastos.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CGASTOSContext _context;

        private IParametroRepository _parametro = default!;
        private IUsuarioRepository _usuario = default!;

        public IParametroRepository Parametro => _parametro ?? new ParametroRepository(_context);
        public IUsuarioRepository Usuario => _usuario ?? new UsuarioRepository(_context);

        public UnitOfWork(CGASTOSContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
