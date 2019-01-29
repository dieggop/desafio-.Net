using System;
using System.Net;
using System.Net.Http;
using desafio_.Net.Configuracoes;
using desafio_.Net.Exceptions;
using desafio_.Net.Models;
using desafio_.Net.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace desafio_.Net.Controllers
{
    [Produces("application/json")]
    // [Route("api/usuarios")]
    public class UsuariosController : Controller
    {

        private readonly IUsuariosServices _userService;
        public UsuariosController(IUsuariosServices userServ )
        {
            _userService = userServ;
        }

        [Authorize()]
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll() {

            return new OkObjectResult(_userService.GetAll());

        }

        [Authorize()]
        [HttpGet("{id}", Name="getUsuario")]
        [Route("id")]
        public IActionResult GetById(int id)
        {
            Usuario retorno;
            try {
                retorno = _userService.Find(id);
            } catch (ExceptionExists e) {
                return NotFound(new {message = e.Message, 
                errorCode = 404});
            }

            return new ObjectResult(retorno);
        }
        
        [Authorize()]
        [HttpGet]
        [Route("me")]
        public IActionResult GetMe()
        {
            Usuario retorno;
            try {
                retorno = _userService.ShowMe();
            } catch (ExceptionExists e) {
                return NotFound(new {message = e.Message, 
                errorCode = 404});
            }
            
            return new ObjectResult(retorno);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("signup")]
        public IActionResult Create([FromBody] Usuario usuario) {

            if (usuario == null) BadRequest();

            try {
                _userService.Add(usuario);
            } catch(ExceptionOfBusiness e ) {
                return BadRequest(new {message = e.Message, 
                errorCode = 400});
            } catch(ExceptionExists e) {
                return BadRequest(new {message = e.Message, 
                errorCode = 400});
            }

            return Ok("You have successfully signed up");
        }

        [Authorize()]
        [HttpDelete("{id}")]
        [Route("delete")]
        public IActionResult Remove(int id) {
            
            try {
                _userService.Remove(id);
            } catch (ExceptionExists e) {
                return NotFound(new {message = e.Message, 
                errorCode = 404});
            }

            return new NoContentResult();
        }

        [Authorize()]
        [HttpPut("{id}")]
        [Route("update")]
        public IActionResult Update([FromBody] Usuario usuario, long id) {

            if (usuario == null || usuario.UsuarioID != id) {
                return BadRequest(new {message = "Payload error", 
                errorCode = 400});
            }
            try {
                _userService.Update(usuario);
            } catch(ExceptionOfBusiness e ) {
                return BadRequest(new {message = e.Message, 
                errorCode = 400});
            } catch(ExceptionExists e) {
                return BadRequest(new {message = e.Message, 
                errorCode = 400});
            } catch(SqliteException e) {
                return BadRequest(new {message = e.Message, 
                errorCode = 400});
            }catch(DbUpdateException e) {
                return BadRequest(new {message = e.Message, 
                errorCode = 400});
            }

            return new NoContentResult();
        }
    }
}