using desafio_.Net.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_.Net.Contexts
{
    public class DesafioDbContext : DbContext
    {
        public DesafioDbContext(DbContextOptions<DesafioDbContext> options) : base(options) {
        }
        
        public DbSet<Usuario> Usuarios {set; get;}
        public DbSet<Phone> Phones {set; get;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Phones)
                .OnDelete(DeleteBehavior.Cascade);;

            modelBuilder.Entity<Usuario>()
                .Property(u => u.UsuarioID)
                .ValueGeneratedOnAdd();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=baseSqLite.db");
    }

    }
}