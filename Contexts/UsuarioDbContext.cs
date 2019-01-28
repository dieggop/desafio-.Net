using desafio_.Net.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_.Net.Contexts
{
    public class UsuarioDbContext : DbContext
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options) {
        }
        
        public DbSet<Usuario> Usuarios {set; get;}
        public DbSet<Phone> Phones {set; get;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Phones);

            modelBuilder.Entity<Phone>()
            .HasKey(p => p.number);
            
            modelBuilder.Entity<Usuario>()
                .Property(u => u.UsuarioID)
                .ValueGeneratedOnAdd();
        }

    }
}