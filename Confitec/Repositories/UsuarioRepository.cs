using Confitec.Data;
using Confitec.Models;
using Microsoft.EntityFrameworkCore;

namespace Confitec.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Usuario usuario);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _context;

        public UsuarioRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task AddAsync(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }

}
