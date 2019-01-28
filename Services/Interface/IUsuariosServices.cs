using System.Collections.Generic;
using desafio_.Net.Models;

namespace desafio_.Net.Services
{
    public interface IUsuariosServices
    {
        int Add(Usuario user);
        IEnumerable<Usuario> GetAll();
        Usuario Find(long id);
        void Remove(long id);
        void Update(Usuario user);
    }
}