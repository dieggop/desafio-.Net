using System.Collections.Generic;

namespace desafio_.Net.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<Phone> phones { get; set; }
    }
}