using System.Collections.Generic;
using desafio_.Net.Models;

namespace desafio_.Net.Repository.Interface
{
    public interface IUsuarioRepository
    {
        void Add(Usuario user);
        IEnumerable<Usuario> GetAll();
        Usuario Find(long id);
        IEnumerable<Usuario> FindByEmail(string email);
        void Remove(long id);
        void Update(Usuario user);

    }
}