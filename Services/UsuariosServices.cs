using System.Collections.Generic;
using desafio_.Net.Models;
using desafio_.Net.Repository;

namespace desafio_.Net.Services
{
    public class UsuariosServices : IUsuariosServices
    {
        private readonly IUsuarioRepository _repositorio;
        public UsuariosServices(IUsuarioRepository rep)
        {
            _repositorio = rep;
        }

        public int Add(Usuario user)
        {
            throw new System.NotImplementedException();
        }

        public Usuario Find(long id)
        {
            Usuario retorno = new Usuario();
            retorno.firstName = "nome";
            retorno.UsuarioID = 1;
            retorno.lastName = "fim nome";

            return retorno;
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