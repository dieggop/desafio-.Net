using System.ComponentModel.DataAnnotations;

namespace desafio_.Net.Models
{
    public class Phone
    {
        [Key]
        public int number { get; set; }
        public int area_code { get; set; }
        public int coutry_code { get; set; }

        public Usuario Usuario {get; set;}
    }
}