using desafio_.Net.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_.Net.Contexts
{
    public class UsuarioDbContext : DbContext
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options) {
        }
        
        public DbSet<Usuario> Usuarios {set; get;}

    }
}