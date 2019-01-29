using System.Collections.Generic;
using desafio_.Net.Models;
using desafio_.Net.Models.DTO;

namespace desafio_.Net.Services
{
    public interface IUsuariosServices
    {
        bool Add(Usuario user);
        IEnumerable<Usuario> GetAll();
        Usuario Find(long id);
        Usuario ShowMe();
        IEnumerable<Usuario> FindByEmail(string Email);
        void Remove(long id);
        void Update(Usuario user);

        bool ValidaUsuario(UsuarioLogin login);
    }
}