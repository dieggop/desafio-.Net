using desafio_.Net.Models;
using desafio_.Net.Services;
using Microsoft.AspNetCore.Mvc;

namespace desafio_.Net.Controllers
{
    [Route("api/usuarios")]
    public class UsuariosController : Controller
    {

        private readonly IUsuariosServices _userService;
        public UsuariosController(IUsuariosServices userServ)
        {
            _userService = userServ;
        }

        
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Usuario retorno = _userService.Find(id);
            return retorno.firstName +"-" + retorno.lastName;
        }

    }
}