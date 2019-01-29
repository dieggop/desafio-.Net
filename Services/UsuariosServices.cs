using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using desafio_.Net.Configuracoes;
using desafio_.Net.Exceptions;
using desafio_.Net.Models;
using desafio_.Net.Models.DTO;
using desafio_.Net.Repository;
using desafio_.Net.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace desafio_.Net.Services
{
    public class UsuariosServices : IUsuariosServices
    {
        private readonly IUsuarioRepository _repositorio;
        private readonly IPhoneRepository _repositorioPhone;
        private IPasswordHasher<Usuario> _passwordHasher;
         private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuariosServices(IUsuarioRepository rep, IPhoneRepository pRep, IHttpContextAccessor httpContextAccessor)
        {
            _repositorio = rep;
            _repositorioPhone = pRep;
            _passwordHasher = new ConfigurablePasswordHasher();
            _httpContextAccessor = httpContextAccessor;

        }

        public bool Add(Usuario user)
        {

            checkMandatoryFields(user);

            checkIntegrity(user);

            user.password = _passwordHasher.HashPassword(user, user.password);
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

        public Usuario ShowMe() {
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            Usuario retorno = null;

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token.Replace("Bearer ","")) as JwtSecurityToken;
            Console.WriteLine("Claims");
            
           retorno = _repositorio.FindByEmail(jwt.Claims.ToList()[0].Value).FirstOrDefault();

            if (retorno == null) throw new ExceptionExists("Unauthorized");

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
            if (!string.IsNullOrEmpty(user.password)) {
                retorno.password = _passwordHasher.HashPassword(user, user.password);
            }
            if (retorno.Phones.Count > 0) {
                foreach (Phone p in retorno.Phones.ToList()) {
                    _repositorioPhone.Remove(p.number);
                    retorno.Phones.Remove(p);
                }
            }

            retorno.Phones = user.Phones;

            _repositorio.Update(retorno);
        }

        public bool ValidaUsuario(UsuarioLogin login)
        {

            Usuario retorno = null;
                retorno = _repositorio.FindByEmail(login.email).FirstOrDefault();
            if (retorno == null) {
                 throw new ExceptionExists("Invalid e-mail or password");
            }

            string passHashed =  _passwordHasher.HashPassword(retorno, login.password);

            PasswordVerificationResult logar = _passwordHasher.VerifyHashedPassword(retorno, retorno.password, login.password);

            
            if (logar.Equals(PasswordVerificationResult.Failed)) {
                Console.WriteLine("Senha não bate");
                 throw new ExceptionExists("Invalid e-mail or password");
            }
            return true;
        }
    }
}