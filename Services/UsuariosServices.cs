using System;
using System.Collections.Generic;
using System.Linq;
using desafio_.Net.Exceptions;
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

        public bool Add(Usuario user)
        {

            checkMandatoryFields(user);

            checkIntegrity(user);

            _repositorio.Add(user);
            return true;
        }

        private void checkIntegrity(Usuario user) {
            IEnumerable<Usuario> retorno = this.FindByEmail(user.email);

            if (retorno.Count() > 0) {
                throw new ExceptionExists("E-mail already exists");
            }   
        }
        private void checkMandatoryFields(Usuario user) {
                string msg = "";
                int erros = 0;
                    

                    if (String.IsNullOrEmpty(user.password)) {
                        erros++;
                        msg = String.Concat(msg," [Password] ");
                    }

                    if (String.IsNullOrEmpty(user.email)) {
                        erros++;
                        msg = String.Concat(msg," [Email] ");
                    }

                    if (erros > 0) {
                        msg = String.Concat("Missing fields | ",msg);
                        throw new ExceptionOfBusiness(msg);
                    }
                  
        }
        public IEnumerable<Usuario> FindByEmail(string email) {

            return _repositorio.FindByEmail(email);

        }
        public Usuario Find(long id)
        {
           
           Usuario retorno = null;

           retorno = _repositorio.Find(id);

            return retorno;
        }

        public IEnumerable<Usuario> GetAll()
        {
           return _repositorio.GetAll();
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