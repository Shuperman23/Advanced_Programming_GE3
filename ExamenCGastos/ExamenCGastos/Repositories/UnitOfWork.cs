using ExamenCGastos.Data;
using ExamenCGastos.Interfaces;

namespace ExamenCGastos.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CGASTOSContext _context;

        private IParametroRepository _parametro = default!;

        public IParametroRepository Parametro => _parametro ?? new ParametroRepository(_context);

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