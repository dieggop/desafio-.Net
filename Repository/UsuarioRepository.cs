using System.Collections.Generic;
using System.Linq;
using desafio_.Net.Contexts;
using desafio_.Net.Models;
using desafio_.Net.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace desafio_.Net.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly DesafioDbContext _contextDb;
        public UsuarioRepository(DesafioDbContext ctx)
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
            return _contextDb.Usuarios.Include(u => u.Phones).FirstOrDefault(u => u.UsuarioID == id);
        }
 
        public IEnumerable<Usuario> FindByEmail(string busca)
        {
            return _contextDb.Usuarios.Where(
                u => u.email.Contains(busca)
            );
        }

        public Usuario FindUserByEmailAndPassword(string email, string password)
        {
            return _contextDb.Usuarios.SingleOrDefault(x => x.email == email && x.password==password);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _contextDb.Usuarios.Include(u => u.Phones).ToList();
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