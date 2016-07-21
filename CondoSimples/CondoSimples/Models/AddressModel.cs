using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class AddressModel
    {
        public int ID { get; set; }
        [Display(Name = "CEP")]
        public string CEP { get; set; }
        [Display(Name = "Logradouro")]
        public string Street { get; set; }
        [Display(Name = "Nº")]
        public string Number { get; set; }
        [Display(Name = "Cidade")]
        public string City { get; set; }
        [Display(Name = "Estado")]
        public string State { get; set; }
    }
}
