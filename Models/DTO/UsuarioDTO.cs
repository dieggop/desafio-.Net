using System.Collections.Generic;

namespace desafio_.Net.Models.DTO
{
    public class UsuarioDTO
    {
        public string firstName  { get; set; }
        public string lastName  { get; set; }
        public string email  { get; set; }
        public ICollection<Phone> phones  { get; set; }
        public string created_at  { get; set; }
        public string last_login  { get; set; }
    }
}