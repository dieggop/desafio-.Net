using System.Collections.Generic;
using desafio_.Net.Models;
using desafio_.Net.Repository;

namespace desafio_.Net.Services.Impl
{
    public class UsuariosServices : IUsuariosServices
    {
        private readonly UsuarioRepository _repositorio;
        public UsuariosServices(UsuarioRepository rep)
        {
            _repositorio = rep;
        }

        public int Add(Usuario user)
        {
            throw new System.NotImplementedException();
        }

        public Usuario Find(long id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Remove(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Usuario user)
        {
            throw new System.NotImplementedException();
        }
    }
}