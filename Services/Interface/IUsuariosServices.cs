using System.Collections.Generic;
using desafio_.Net.Models;

namespace desafio_.Net.Services
{
    public interface IUsuariosServices
    {
        bool Add(Usuario user);
        IEnumerable<Usuario> GetAll();
        Usuario Find(long id);
        IEnumerable<Usuario> FindByEmail(string Email);
        void Remove(long id);
        void Update(Usuario user);
    }
}