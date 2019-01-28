using System.Collections.Generic;
using System.Linq;
using desafio_.Net.Contexts;
using desafio_.Net.Models;

namespace desafio_.Net.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly UsuarioDbContext _contextDb;
        public UsuarioRepository(UsuarioDbContext ctx)
        {
            _contextDb = ctx;
        }

        public void Add(Usuario user)
        {
            _contextDb.Usuarios.Add(user);
            _contextDb.SaveChanges();
        }

        public Usuario Find(long id)
        {
            return _contextDb.Usuarios.FirstOrDefault(u => u.UsuarioID == id);
        }

        public IEnumerable<Usuario> FindByEmail(string busca)
        {
            return _contextDb.Usuarios.Where(
                u => u.email.Contains(busca)
            );
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _contextDb.Usuarios.ToList();
        }

        public void Remove(long id)
        {
            var entity = _contextDb.Usuarios.First(u => u.UsuarioID == id);
            _contextDb.Usuarios.Remove(entity);
            _contextDb.SaveChanges();
        }

        public void Update(Usuario user)
        {
            _contextDb.Usuarios.Update(user);
            _contextDb.SaveChanges();
        }
    }
}