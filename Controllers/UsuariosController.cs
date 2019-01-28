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

        

    }
}