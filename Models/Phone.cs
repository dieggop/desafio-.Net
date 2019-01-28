using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace desafio_.Net.Models
{
    [Table("Phones")] 
    public class Phone
    {
        [Key]
        public int number { get; set; }
        public int area_code { get; set; }
        public int country_code { get; set; }

        [DataMember]
        public Usuario Usuario {get; set;}
    }
}