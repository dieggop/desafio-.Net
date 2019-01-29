using System.Security.Claims;
using desafio_.Net.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using desafio_.Net.Services;
using desafio_.Net.Exceptions;

namespace desafio_.Net.Controllers
{
    public class TokenController : Controller
    {
        
        private readonly IConfiguration _configuration;
        private readonly IUsuariosServices _userService;
        public TokenController(IConfiguration configuration, IUsuariosServices userServ)
        {
            _userService = userServ;
            _configuration = configuration;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken([FromBody] UsuarioLogin request)
        {

            try {
                bool logar = _userService.ValidaUsuario(request);

            
                var claims = new[]
                {
                     new Claim(ClaimTypes.Name, request.email)
                };

               var key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));

               var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 

               var token = new JwtSecurityToken(
                    issuer: "desafio.net", 
                    audience: "pitang.net",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            } catch (ExceptionExists e) {
                return BadRequest(new {message = "Invalid e-mail or password", 
                    errorCode = 400});
            }
        }
    }
}