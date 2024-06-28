using ControlBiblioteca.Data;
using ControlBiblioteca.Interfaces;
using ControlBiblioteca.Models;
using ControlBiblioteca.Repositories;

namespace ControlBiblioteca.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BIBLIOTECAContext _context;

        private IAutorRepository _autor = default!;//forma de decir que no siempre sera null
        private IGeneroLiterarioRepository _generoLiterario = default!;
        private ILibroRepository _libro = default!;
        private IParametroRepository _parametro = default!;

        public IAutorRepository Autor => _autor ?? new AutorRepository(_context);//?? significa que sea null o vacio
        public IGeneroLiterarioRepository GeneroLiterario => _generoLiterario ?? new GeneroLiterarioRepository(_context);
        public ILibroRepository Libro => _libro ?? new LibroRepository(_context);
        public IParametroRepository Parametro => _parametro ?? new ParametroRepository(_context);

        public UnitOfWork(BIBLIOTECAContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
