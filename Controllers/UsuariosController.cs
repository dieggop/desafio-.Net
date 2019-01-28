using System;
using System.Net;
using System.Net.Http;
using desafio_.Net.Exceptions;
using desafio_.Net.Models;
using desafio_.Net.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace desafio_.Net.Controllers
{
    [Produces("application/json")]
    [Route("api/usuarios")]
    public class UsuariosController : Controller
    {

        private readonly IUsuariosServices _userService;
        public UsuariosController(IUsuariosServices userServ)
        {
            _userService = userServ;
        }

        [HttpGet]
        public IActionResult GetAll() {

            return new OkObjectResult(_userService.GetAll());

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Usuario retorno = _userService.Find(id);
            
            if (retorno == null) { return NotFound(); }

            return new ObjectResult(retorno);
        }

        [HttpPost]
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

            return Ok();
        }

    }
}