using System;
using System.Collections.Generic;
using System.Linq;
using desafio_.Net.Exceptions;
using desafio_.Net.Models;
using desafio_.Net.Repository;
using desafio_.Net.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace desafio_.Net.Services
{
    public class UsuariosServices : IUsuariosServices
    {
        private readonly IUsuarioRepository _repositorio;
        private readonly IPhoneRepository _repositorioPhone;
        public UsuariosServices(IUsuarioRepository rep, IPhoneRepository pRep)
        {
            _repositorio = rep;
            _repositorioPhone = pRep;
        }

        public bool Add(Usuario user)
        {

            checkMandatoryFields(user);

            checkIntegrity(user);

            try {
                _repositorio.Add(user);
            } catch (DbUpdateException e) {
                throw new ExceptionOfBusiness(e.Message);
            }
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

            if (retorno == null) throw new ExceptionExists("Not Found");

            return retorno;
        }

        public IEnumerable<Usuario> GetAll()
        {
           return _repositorio.GetAll();
        }

        public void Remove(long id)
        {

            Usuario retorno = null;

            retorno = _repositorio.Find(id);

            if (retorno == null) throw new ExceptionExists("Not Found");

            _repositorio.Remove(id);
        }

        public void Update(Usuario user)
        {
            Usuario retorno = null;
                retorno = _repositorio.Find(user.UsuarioID);

            if (retorno == null) throw new ExceptionExists("Not Found");

            retorno.email = user.email;
            retorno.firstName = user.firstName;
            retorno.lastName = user.lastName;
            retorno.password = user.password;
            
            if (retorno.Phones.Count > 0) {
                foreach (Phone p in retorno.Phones.ToList()) {
                    _repositorioPhone.Remove(p.number);
                    retorno.Phones.Remove(p);
                }
            }

            retorno.Phones = user.Phones;

            _repositorio.Update(retorno);
        }
    }
}