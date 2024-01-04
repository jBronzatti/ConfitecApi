using Confitec.Models;
using Microsoft.EntityFrameworkCore;

namespace Confitec.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
    }
}
